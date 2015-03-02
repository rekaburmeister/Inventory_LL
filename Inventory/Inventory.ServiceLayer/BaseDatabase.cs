using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Data;

namespace Inventory.ServiceLayer
{
    public class BaseDatabase
    {
        public ICollection<IInventoryItem> InventoryItems { get; set; }

        public ICollection<Category> Categories { get; set; }
        protected InventoryHandler InventoryHandler { get; set; }

        public BaseDatabase(List<IInventoryItem> items)
        {
            Categories = new List<Category>();
            InventoryItems = items;
            InventoryHandler = new InventoryHandler(InventoryItems);
        }

        public void AddItem(IInventoryItem item)
        {
            if (!Categories.Any(c => c.Name.Equals(item.Category)))
            {
                throw new Exception(string.Format("The '{0}' category is not found in the database", item.Category));
            }

            InventoryHandler.AddItem(item);
        }

        public IInventoryItem FindItem(string name, string category)
        {
            return InventoryHandler.FindItem(name, category);
        }

        public bool HasItem(string name, string category)
        {
            return InventoryHandler.HasItem(name, category);
        }

        public void ModifyStockValue(string name, string category, int modifyBy)
        {
            InventoryHandler.UpdateItemStock(name, category, modifyBy);
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
