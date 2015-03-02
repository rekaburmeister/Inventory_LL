using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Data
{
    public class MerchantItem : InventoryItem
    {
        public Supplier PreferredSupplier { get; set; }
        public Supplier FailoverSupplier { get; set; }
        public int PurchasedPrice { get; set; }

        public MerchantItem(string name, string category, int price, int stock) : base(name, category, price, stock)
        {
        }
    }
}
