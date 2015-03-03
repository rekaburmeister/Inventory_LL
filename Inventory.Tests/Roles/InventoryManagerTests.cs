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
            var item = new InventoryItem {Name = "Item 1", Category = "Cat", Price = 2.3, Stock = 1};
            manager.AddItem(item);
            manager.AddItem(item);
            Assert.AreEqual(2, manager.GetItemStock(item), "Stock is correct");
        }

        [TestCase(5, 2, 3)]
        [TestCase(4, 3, 1)]
        [TestCase(1, 1, 0)]
        [TestCase(0, 1, 0)]
        [TestCase(2, 3, 0)]
        public void DecreaseItemStock(int original, int decreaseBy, int expectedNumber)
        {
            InventoryManager manager = new InventoryManager();
            var item = new InventoryItem { Name = "Item 1", Category = "Cat", Price = 2.3, Stock = original };
            manager.AddItem(item);
            manager.DecreaseItemStock(item, decreaseBy);
            Assert.AreEqual(expectedNumber, manager.GetItemStock(item), "Stock is correct");
        }
    }
}
