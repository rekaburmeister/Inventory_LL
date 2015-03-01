using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Data
{
    public class Merchant
    {
        public string Name { get; set; }
        public InventoryObject InventoryObject { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}
