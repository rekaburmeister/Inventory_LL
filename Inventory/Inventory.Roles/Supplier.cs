using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Roles.Extensions;

namespace Inventory.Roles
{
    public class Supplier : IEquatable<Supplier>
    {
        private Guid SupplierId { get; set; }
        public string SupplierName { get; private set; }
        private InventoryManager InventoryManager { get; set; }

        public Supplier(string name)
        {
            SupplierId = Guid.NewGuid();
            SupplierName = name.ValidateInput();
            InventoryManager = new InventoryManager();
        }

        public void AddToInventory(InventoryItem item)
        {
            InventoryManager.AddItem(item);
        }

        public void PlaceOrder(Order order)
        {
            
        }

        public bool Equals(Supplier other)
        {
            return other.SupplierId.Equals(SupplierId);
        }
    }
}
