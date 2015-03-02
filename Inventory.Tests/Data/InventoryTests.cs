using System;
using System.Collections.Generic;
using NUnit.Framework;
using Inventory.Data;

namespace Inventory.Tests.Data
{
    [TestFixture]
    public class InventoryTests
    {
        private InventoryHandler m_Inventory;
        private SupplierItem m_ExistantItem;
        private SupplierItem m_NonExistantItem;

        private const string c_Item1 = "item 1";
        private const string c_Item2 = "item 2";
        private const string c_Category = "category 1";

        [TestFixtureSetUp]
        public void Setup()
        {
            m_Inventory = new InventoryHandler(new List<IInventoryItem>());
            m_ExistantItem = new SupplierItem(c_Item1, c_Category, 1, 1);
            m_NonExistantItem = new SupplierItem(c_Item2, c_Category, 1, 1);
        }

        [TearDown]
        public void TearDown()
        {
            m_Inventory = new InventoryHandler(new List<IInventoryItem>());
        }

        [Test]
        public void AddAndRemoveItem()
        {
            Assert.DoesNotThrow(() => m_Inventory.AddItem(m_ExistantItem), "Couldn't add item");
            Assert.AreEqual(1, m_Inventory.Count, "Item count incorrect");
            Assert.IsTrue(
                m_Inventory.HasItem(m_ExistantItem.Name, m_ExistantItem.Category), 
                "Couldn't find item");
            Assert.DoesNotThrow(() => m_Inventory.RemoveItem(m_ExistantItem), "Couldn't remove item");
            Assert.AreEqual(0, m_Inventory.Count, "Item count incorrect after remove");
            Assert.IsFalse(
                m_Inventory.HasItem(m_ExistantItem.Name, m_ExistantItem.Category),
                "Inventory still has the item");
        }

        [Test]
        public void ExistanceCheck()
        {
            m_Inventory.AddItem(m_ExistantItem);
            Assert.Throws(
                typeof (Exception),
                () => m_Inventory.AddItem(new SupplierItem(c_Item1, c_Category, 2, 2)),
                "Could add item with same name and category");

            var newItem = new SupplierItem(c_Item1, "new category", 1, 1);
            var newItem2 = new SupplierItem("new name",c_Category, 1, 1);
            Assert.DoesNotThrow(() => m_Inventory.AddItem(newItem), "Couldn't add item with different category");
            Assert.DoesNotThrow(() => m_Inventory.AddItem(newItem2), "Couldn't add item with different name");
        }

        [Test]
        public void InventoryItemExistenceCheck()
        {
            m_Inventory.AddItem(m_ExistantItem);

            Assert.DoesNotThrow(
                () => m_Inventory.CheckItemInInventory(m_ExistantItem), 
                "No exception when checking for existence and exists");

            Assert.Throws(
                typeof(Exception),
                () => m_Inventory.CheckItemInInventory(m_NonExistantItem), 
                "Exception when checking for existence and doesn't exist");

            Assert.DoesNotThrow(
                () => m_Inventory.CheckItemInInventory(m_NonExistantItem, false),
                "No exception when checking for absence and doesn't exist");

            Assert.Throws(
                typeof(Exception),
                () => m_Inventory.CheckItemInInventory(m_ExistantItem, false), 
                "Exception when checking for absence and exists");
        }

        [Test]
        public void UpdateStock()
        {
            m_Inventory.AddItem(m_ExistantItem);
            m_Inventory.UpdateItemStock(c_Item1, c_Category, 5);
            var item = m_Inventory.FindItem(c_Item1, c_Category);

            Assert.AreEqual(6, item.Stock, "The stock hasn't been updated");

            m_Inventory.UpdateItemStock(c_Item1, c_Category, -2);
            Assert.AreEqual(4, item.Stock, "The stock hasn't been updated");
        }

        [Test]
        public void GetItemStock()
        {
            m_Inventory.AddItem(m_ExistantItem);
            int stock = m_Inventory.GetItemStock(c_Item1, c_Category);

            Assert.AreEqual(1, stock, "Stock is incorrect");
        }
    }
}
