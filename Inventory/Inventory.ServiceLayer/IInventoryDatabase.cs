using System.Collections.Generic;
using Inventory.Data;

namespace Inventory.ServiceLayer
{
    public interface IInventoryDatabase
    {
        ICollection<InventoryItem> InventoryItems { get; set; }
        ICollection<Category> Categories { get; set; }

        void AddItem(InventoryItem item);
        InventoryItem FindItem(string name, string category);
        int GetNumberOfInventoryItems();

        void AddCategory(string categoryName);
        IEnumerable<string> GetCategoryNames();
    }
}