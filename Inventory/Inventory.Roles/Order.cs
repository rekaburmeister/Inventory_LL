using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Roles
{
    public class Order
    {
        public ICollection<InventoryItem> Goods { get; set; } // this shouldn't be InventoryItem but it will do for now
    }
}
