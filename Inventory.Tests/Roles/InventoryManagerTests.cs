using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Roles;
using NUnit.Framework;

namespace Inventory.Tests.Roles
{
    [TestFixture]
    class InventoryManagerTests
    {
        [Test]
        public void AddExistingItemToInventory()
        {
            InventoryManager manager = new InventoryManager();
            var item = new InventoryItem() {Name = "Item 1", Category = "Cat", Price = 2.3, Stock = 1};
            manager.AddItem(item);
            manager.AddItem(item);
            Assert.AreEqual(2, manager.GetItemStock(item), "Stock is correct");
        }
    }
}
