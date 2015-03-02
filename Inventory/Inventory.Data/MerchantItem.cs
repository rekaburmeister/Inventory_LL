using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Data
{
    public class MerchantItem : IInventoryItem
    {
        public string Name { get; private set; }
        public string Category { get; private set; }
        public int Price { get; set; }
        public int Stock { get; set; }

        public Supplier PreferredSupplier { get; set; }
        public Supplier FailoverSupplier { get; set; }
        public int PurchasedPrice { get; set; }

        public MerchantItem(string name, string category, int price, int stock)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name can't be null or empty");
            }

            if (string.IsNullOrEmpty(category))
            {
                throw new ArgumentException("Category can't be null or empty");
            }

            if (price < 0)
            {
                throw new ArgumentException("Price has to be at least 0");
            }

            if (stock < 0)
            {
                throw new ArgumentException("Stock has to be at least 0");
            }

            Name = name;
            Category = category;
            Price = price;
            Stock = stock;
        }

        
    }
}
