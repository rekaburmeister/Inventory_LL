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
        public void AddItemToInventory()
        {
            InventoryManager manager = new InventoryManager();
            var item = new InventoryItem("Item 1", "Cat", 2.3, 3);
            manager.AddItem(item);
            Assert.AreEqual(3, manager.GetItemStock(item), "Adding an item with an initial stock works");
        }
        
        [Test]
        public void AddExistingItemToInventory()
        {
            InventoryManager manager = new InventoryManager();
            var item = new InventoryItem ("Item 1", "Cat", 2.3, 1);
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
            var item = new InventoryItem ( "Item 1", "Cat", 2.3, original );
            manager.AddItem(item);
            manager.DecreaseItemStock(item, decreaseBy);
            Assert.AreEqual(expectedNumber, manager.GetItemStock(item), "Stock is correct");
        }

        [TestCase(-1, 1)]
        [TestCase(-0.1, 1)]
        [TestCase(0, 1)]
        [TestCase(0, 0)]
        [TestCase(1, -1)]
        public void InvalidItem(double price, int stock)
        {
            Assert.Throws(typeof(ArgumentException), () => { new InventoryItem("name", "category", price, stock); });
        }
    }
}
