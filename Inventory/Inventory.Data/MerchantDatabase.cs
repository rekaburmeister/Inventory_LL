using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Data
{
    public class MerchantDatabase
    {
        public List<Customer> Customers { get; set; }
        public List<Category> Categories { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public InventoryObject InventoryObject { get; set; }

        public void AddCategory(string categoryName)
        {
            Categories.Add(new Category{Name = categoryName});
        }

        public void AddInventoryItem(InventoryItem item)
        {
            if (!Categories.Any(c => c.Name.Equals(item.Category)))
            {
                throw new Exception("The specified category is not found in the database");
            }

        }


    }
}
