using System.Collections.Generic;
using Inventory.Data;

namespace Inventory.ServiceLayer
{
    public interface IInventoryDatabase
    {
        ICollection<InventoryItem> InventoryItems { get; set; }
        void AddItem(InventoryItem item);
        InventoryItem GetItem(string name);
        void UpdateStock(string name, int stock);
    }
}