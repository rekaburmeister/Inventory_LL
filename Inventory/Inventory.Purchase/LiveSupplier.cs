using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Data;
using Inventory.ServiceLayer;

namespace Inventory.Purchase
{
    public class LiveSupplier
    {
        public string Name { get; set; }
        private SupplierDatabase Database { get; set; }

        public LiveSupplier(string supplier)
        {
            Name = supplier;
            Database = new SupplierDatabase();
        }

        public List<IInventoryItem> InventoryItems()
        {
            return Database.InventoryItems.ToList();
        }

        public void AddCategory(string category)
        {
            Database.AddCategory(category);
        }

        public void AddItem(SupplierItem item)
        {
            Database.AddItem(item);
        }

        public bool HasStock(string name, string category)
        {
            return Database.InventoryItems.Any(i => i.Stock > 0 && i.Name.Equals(name) && i.Category.Equals(category));
        }

        public int StockOf(string item, string cat)
        {
            if (Database.HasItem(item, cat))
            {
                return Database.InventoryItems.Single(i => i.Category.Equals(cat) && i.Name.Equals(item)).Stock;
            }
            else
            {
                return 0;
            }
        }

        public void ModifyStockValue(string name, string category, int modifyBy)
        {
            Database.ModifyStockValue(name, category, modifyBy);
        }
    }
}
