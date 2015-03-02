using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Data;
using Inventory.Exceptions;
using Inventory.ServiceLayer;

namespace Inventory.Purchase
{
    public class Merchant
    {
        private MerchantDatabase Database { get; set; }
        private ICollection<LiveSupplier> Supliers { get; set; } 

        public Merchant()
        {
            Database = new MerchantDatabase();
            Supliers = new List<LiveSupplier>();
        }

        public void InitiliseSuppliers(IEnumerable<LiveSupplier> suppliers)
        {
            foreach (LiveSupplier liveSupplier in suppliers)
            {
                if (!Supliers.Any(s => s.Name.Equals(liveSupplier.Name)))
                {
                    Supliers.Add(liveSupplier);
                }
                var availableItems = liveSupplier.InventoryItems();
                Database.ExtendCategories(availableItems.Select(i=>i.Category));
                foreach (IInventoryItem availableItem in availableItems)
                {
                    if (Database.HasItem(availableItem.Name, availableItem.Category))
                    {
                        Database.ModifyStockValue(availableItem.Name, availableItem.Category, availableItem.Stock);
                        if (!Database.DoesItemHaveFailoverSupplier(availableItem.Name, availableItem.Category))
                        {
                            Database.SetFailoverSupplierForItem(availableItem.Name, availableItem.Category, new Supplier(liveSupplier.Name));
                        }
                    }
                    else
                    {
                        MerchantItem newItem = new MerchantItem(availableItem.Name, availableItem.Category, availableItem.Stock, availableItem.Price)
                        {
                            PreferredSupplier = new Supplier(liveSupplier.Name)
                        };
                        Database.AddItem(newItem);
                    }
                }
            }
        }

        public void AddCustomer(LiveCustomer liveCustomer)
        {
            Customer liveCustomerDbObject = new Customer(liveCustomer.UserName, liveCustomer.Password);
            Database.AddCustomer(liveCustomerDbObject);
        }

        public bool AuthenticateUser(string livecustomer, string password)
        {
            Customer customer;
            try
            {
                customer = Database.FindCustomer(livecustomer);
            }
            catch (InvalidOperationException e)
            {
                return false;
            }
            return customer.HasPassword(password);
        }

        public bool BuyItem(string itemName, string category)
        {
            if (string.IsNullOrEmpty(itemName))
            {
                throw new ArgumentException("Item name can't be null or empty");
            }

            if (string.IsNullOrEmpty(category))
            {
                throw new ArgumentException("Category can't be null or empty");
            }

            if (!Database.HasItem(itemName, category))
            {
                return false;
            }

            var item = Database.FindItem(itemName, category);
            if (item.Stock == 0)
            {
                return false;
            }
            
            return HandlePurchase(item);
        }

        private bool HandlePurchase(IInventoryItem item)
        {
            LiveSupplier usedSupplier = GetSupplierForPurchase(item);
            usedSupplier.ModifyStockValue(item.Name, item.Category, -1);
            Database.ModifyStockValue(item.Name, item.Category, -1);
            return true;
        }

        private LiveSupplier GetSupplierForPurchase(IInventoryItem item)
        {
            LiveSupplier suplier = Supliers.Single(s => s.Name.Equals(item.PreferredSupplier.Name));
            if (suplier.HasStock(item.Name, item.Category))
            {
                return suplier;
            }
            
            suplier = Supliers.Single(s => s.Name.Equals(item.FailoverSupplier.Name));
            if (suplier.HasStock(item.Name, item.Category))
            {
                return suplier;
            }

            return Supliers.FirstOrDefault(s => s.HasStock(item.Name, item.Category));
        }
    }
}
