using System.Collections.Generic;
using Inventory.Data;

namespace Inventory.ServiceLayer
{
    public class SupplierDatabase : BaseDatabase
    {
        public SupplierDatabase() : base(new List<IInventoryItem>())
        {
        }

        public void UpdateStock(string name, string category, int stock)
        {
            InventoryHandler.UpdateItemStock(name, category, stock);
        }
    }
}
