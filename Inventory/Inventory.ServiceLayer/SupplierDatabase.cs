using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Data;

namespace Inventory.ServiceLayer
{
    class SupplierDatabase : IInventoryDatabase
    {
        public ICollection<InventoryItem> InventoryItems { get; set; }
        private InventoryHandler InventoryHandler { get; set; }
        
        public SupplierDatabase()
        {
            InventoryItems = new List<InventoryItem>();
        }

        public void AddItem(InventoryItem item)
        {
            throw new NotImplementedException();
        }

        public InventoryItem GetItem(string name)
        {
            throw new NotImplementedException();
        }

        public void UpdateStock(string name, int stock)
        {
            throw new NotImplementedException();
        }

    }
}
