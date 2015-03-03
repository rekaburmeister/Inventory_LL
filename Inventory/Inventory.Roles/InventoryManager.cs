using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Roles
{
    public class InventoryManager
    {
        private ICollection<InventoryItem> InventoryItems { get; set; }

        public InventoryManager()
        {
            InventoryItems = new List<InventoryItem>();
        }

        public void AddItem(InventoryItem item)
        {
            if (!InventoryItems.Contains(item))
            {
                InventoryItems.Add(item);
            }
            else
            {
                var existingItem = InventoryItems.Single(i => i.Equals(item));
                existingItem.Stock ++;
            }
        }

        public int GetItemStock(InventoryItem item)
        {
            return InventoryItems.Single(i => i.Equals(item)).Stock;
        }
    }
}
