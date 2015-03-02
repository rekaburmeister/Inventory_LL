using System;
using Inventory.Data;
using NUnit.Framework;

namespace Inventory.Tests.Data
{
    [TestFixture]
    public class InventoryItemTests
    {
        [Test]
        public void CreateInventoryItem()
        {
            const string c_ItemName = "item1";
            const string c_CategoryName = "category";
            const int c_Price = 1;
            const int c_Stock = 1;
            InventoryItem item = new InventoryItem(c_ItemName, c_CategoryName, c_Price, c_Stock);

            Assert.AreEqual(c_ItemName, item.Name, "Item name does not match");
            Assert.AreEqual(c_CategoryName, item.Category, "Item category does not match");
            Assert.AreEqual(c_Price, item.Price, "Item price does not match");
               Assert.AreEqual(c_Stock, item.Stock, "Item stock does not match");
        }

        [TestCase(null, TestName = "Null")]
        [TestCase("", TestName = "Empty")] // wouldn't let  me do string.Empty here, value must be constant
        public void ItemNameNullOrEmptyThrows(string itemName)
        {
            Assert.Throws(typeof(ArgumentException), delegate { new InventoryItem(itemName, "category", 1, 1); });
        }

        [TestCase(null, TestName = "Null")]
        [TestCase("", TestName = "Empty")]
        public void ItemCategoryNullOrEmptyThrows(string category)
        {
            Assert.Throws(typeof(ArgumentException), delegate { new InventoryItem("Item1", category, 1,1); });
        }

        [Test]
        public void NegativePrice()
        {
            Assert.Throws(typeof(ArgumentException), delegate { new InventoryItem("Item1", "category", -1, 1); });
        }

        [Test]
        public void NegativeStock()
        {
            Assert.Throws(typeof(ArgumentException), delegate { new InventoryItem("Item1", "category", 1, -1); });
        }
    }
}
