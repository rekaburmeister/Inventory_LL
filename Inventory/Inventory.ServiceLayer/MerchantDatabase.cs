using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Data;

namespace Inventory.ServiceLayer
{
    public class MerchantDatabase
    {
        private ICollection<Customer> Customers { get; set; }
        private ICollection<Category> Categories { get; set; }
        private ICollection<Supplier> Suppliers { get; set; }
        private InventoryObject InventoryObject { get; set; }

        public MerchantDatabase()
        {
            Customers = new List<Customer>();
            Categories = new List<Category>();
            Suppliers = new List<Supplier>();
            InventoryObject = new InventoryObject();
        }

        public void AddCategory(string categoryName)
        {
            if (!Categories.Any(c => c.Name.Equals(categoryName)))
            {
                Categories.Add(new Category {Name = categoryName});
            }
            else
            {
                throw new Exception(string.Format("The '{0}' category already exists", categoryName));
            }
        }

        public IEnumerable<string> GetCategoryNames()
        {
            return Categories.Select(c => c.Name);
        } 

        public void AddInventoryItem(MerchantItem item)
        {
            if (!Categories.Any(c => c.Name.Equals(item.Category)))
            {
                throw new Exception(string.Format("The '{0}' category is not found in the database", item.Category));
            }

            InventoryObject.AddItem(item);
        }

        public int GetNumberOfInventoryItems()
        {
            return InventoryObject.Count;
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
    }
}
