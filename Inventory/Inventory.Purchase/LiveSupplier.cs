using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.ServiceLayer;

namespace Inventory.Purchase
{
    public class LiveSupplier
    {
        public string Name { get; set; }
        public IInventoryDatabase Database { get; set; }

        public LiveSupplier(string supplier)
        {
            Name = supplier;
            Database = new SupplierDatabase();
        }
    }
}
