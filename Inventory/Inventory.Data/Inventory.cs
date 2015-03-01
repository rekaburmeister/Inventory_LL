using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Data
{
    public class Inventory
    {
        public ICollection<InventoryItem> InventoryItems { get; set; }
    }
}
