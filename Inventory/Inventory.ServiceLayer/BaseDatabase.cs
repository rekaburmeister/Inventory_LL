using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Data;

namespace Inventory.ServiceLayer
{
    public class BaseDatabase : IInventoryDatabase
    {
        public ICollection<InventoryItem> InventoryItems { get; set; }

        public ICollection<Category> Categories { get; set; }
        protected InventoryHandler InventoryHandler { get; set; }

        public BaseDatabase()
        {
            Categories = new List<Category>();
            InventoryItems = new List<InventoryItem>();
            InventoryHandler = new InventoryHandler(InventoryItems);
        }

        public void AddItem(InventoryItem item)
        {
            if (!Categories.Any(c => c.Name.Equals(item.Category)))
            {
                throw new Exception(string.Format("The '{0}' category is not found in the database", item.Category));
            }

            InventoryHandler.AddItem(item);
        }

        public InventoryItem GetItem(string name, string category)
        {
            return InventoryHandler.GetItem(name, category);
        }

        public int GetNumberOfInventoryItems()
        {
            return InventoryHandler.Count;
        }

        public void AddCategory(string categoryName)
        {
            if (!Categories.Any(c => c.Name.Equals(categoryName)))
            {
                Categories.Add(new Category { Name = categoryName });
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
    }
}
