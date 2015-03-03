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
            // I'm debating whether the price should be compared here but at the moment I'm against it
            return Name.Equals(other.Name) && Category.Equals(other.Category);
        }
    }
}
