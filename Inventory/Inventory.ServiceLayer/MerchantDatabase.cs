using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Data;

namespace Inventory.ServiceLayer
{
    public class MerchantDatabase : BaseDatabase
    {
        private ICollection<Customer> Customers { get; set; }
        private ICollection<Supplier> Suppliers { get; set; }

        public MerchantDatabase() : base(new List<IInventoryItem>())
        {
            Customers = new List<Customer>();
            Suppliers = new List<Supplier>();
        }

        public void AddCustomer(Customer customer)
        {
            if (!Customers.Any(c => c.UserName.Equals(customer.UserName)))
            {
                Customers.Add(customer);
            }
            else
            {
                throw new Exception(string.Format("Customer '{0}' already exists", customer.UserName));
            }
        }

        public Customer FindCustomer(string userName)
        {
            return Customers.Single(c => c.UserName.Equals(userName));
        }

        public void AddSupplier(Supplier supplier)
        {
            if (!Suppliers.Any(s => s.Name.Equals(supplier.Name)))
            {
                Suppliers.Add(supplier);
            }
            else
            {
                throw new Exception(string.Format("Supplier '{0}' is already in the database", supplier.Name));
            }
        }

        public Supplier FindSupplier(string name)
        {
            return Suppliers.Single(s => s.Name.Equals(name));
        }

        public void ExtendCategories(IEnumerable<string> newCategories)
        {
            foreach (string category in newCategories)
            {
                if (! Categories.Any(c => c.Name.Equals(category)))
                {
                    Categories.Add(new Category{Name = category});
                }
            }
        }


        public bool DoesItemHaveFailoverSupplier(string itemName, string category)
        {
            var item = InventoryHandler.FindItem(itemName, category);
            return item.FailoverSupplier != null;
        }

        public void SetFailoverSupplierForItem(string name, string category, Supplier supplier)
        {
            var item = InventoryHandler.FindItem(name, category);
            item.FailoverSupplier = supplier;
        }
    }
}
