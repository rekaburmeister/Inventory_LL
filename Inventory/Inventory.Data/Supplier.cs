using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Data
{
    public class Supplier
    {
        public string Name { get; set; }
        public InventoryObject InventoryObject { get; set; }

        public Supplier(string supplierName)
        {
            Name = supplierName;
            InventoryObject = new InventoryObject();
        }
    }
}
