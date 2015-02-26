using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Data
{
    public class InventoryItem
    {
        public string Name { get; private set; }
        public string Category { get; private set; }
        public int Price { get; set; }
        public int Stock { get; set; }

        public InventoryItem(string name, string category, int price, int stock)
        {
            Name = name;
            Category = category;
            Price = price;
            Stock = stock;
        }
    }
}
