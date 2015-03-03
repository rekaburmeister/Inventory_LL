using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Roles
{
    public class InventoryItem : IEquatable<InventoryItem>
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }

        public bool Equals(InventoryItem other)
        {
            return Name.Equals(other.Name) && Category.Equals(other.Category);
        }
    }
}
