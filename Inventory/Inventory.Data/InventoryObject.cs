using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.Data
{
    public class InventoryObject
    {
        private ICollection<InventoryItem> InventoryItems { get; set; }

        public int Count { get { return InventoryItems.Count; } }

        public InventoryObject()
        {
            InventoryItems = new List<InventoryItem>();
        }

        public void AddItem(InventoryItem item)
        {
            if (!InventoryItems.Any(i => i.Name.Equals(item.Name) && i.Category.Equals(item.Category)))
            {
                InventoryItems.Add(item);
            }
            else
            {
                throw new Exception(string.Format("Item {0} in {1} category is already in the inventory", item.Name, item.Category));
            }
        }

        public void RemoveItem(InventoryItem item)
        {
            CheckItemInInventory(item);
            InventoryItems.Remove(item);
        }

        public InventoryItem GetItem(string name, string category)
        {
            return InventoryItems.Single(i => i.Name.Equals(name) && i.Category.Equals(category));
        }

        public bool HasItem(string name, string category)
        {
            return InventoryItems.Any(i => i.Name.Equals(name) && i.Category.Equals(category));
        }

        public void UpdateItemStock(string name, string category, int newStock)
        {
            var item = GetItem(name, category);
            item.Stock = newStock;
        }

        public int GetItemStock(string itemName, string category)
        {
            return InventoryItems.Single(i => i.Name.Equals(itemName) && i.Category.Equals(category)).Stock;
        }

        // This function should really be private but I wanted to have a unit test for it. Make private if possible 
        public void CheckItemInInventory(InventoryItem item, bool exists = true)
        {
            string errorMessage =
                exists
                    ? "Item {0} in {1} category is not in the inventory"
                    : "Item {0} in {1} category is already in the inventory";

            if (exists ^ InventoryItems.Contains(item))
            {
                throw new Exception(string.Format(errorMessage, item.Name, item.Category));
            }
        }
    }
}
