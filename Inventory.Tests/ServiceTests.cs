using System;
using System.Linq;
using Inventory.Data;
using Inventory.ServiceLayer;
using NUnit.Framework;

namespace Inventory.Tests
{
    [TestFixture]
    public class ServiceTests
    {
        private MerchantDatabase m_MerchantDatabase;

        [TestFixtureSetUp]
        public void Setup()
        {
            m_MerchantDatabase = new MerchantDatabase();
        }

        [TearDown]
        public void TearDown()
        {
            m_MerchantDatabase = new MerchantDatabase();    
        }

        [Test]
        public void AddCategories()
        {
            const string c_Category1 = "Category 1";
            m_MerchantDatabase.AddCategory(c_Category1);
            var categories = m_MerchantDatabase.GetCategoryNames().ToArray();
            Assert.AreEqual(1, categories.Count(), "Number of categories incorrect (1)");
            Assert.IsTrue(categories.Contains(c_Category1), "Added category missing: " + c_Category1);
            Assert.Throws(
                typeof (Exception), 
                delegate { m_MerchantDatabase.AddCategory(c_Category1); },
                "Could add the same category twice");

            const string c_Category2 = "Category 2";
            m_MerchantDatabase.AddCategory(c_Category2);
            categories = m_MerchantDatabase.GetCategoryNames().ToArray();
            Assert.AreEqual(2, categories.Count(), "Number of categories incorrect (2)");
            Assert.IsTrue(categories.Contains(c_Category2), "Added category missing: " + c_Category2);
        }

        [Test]
        public void InventoryItems()
        {
            const string c_Category1 = "Category 1";
            const string c_Category2 = "Category 2";
            const string c_Item1 = "Item 1";
            const string c_Item2 = "Item 2";

            m_MerchantDatabase.AddCategory(c_Category1);

            Assert.DoesNotThrow(
                delegate { m_MerchantDatabase.AddInventoryItem(new InventoryItem(c_Item1, c_Category1, 1, 1)); },
                "Item couldn't be added");
            Assert.Throws(
                typeof (Exception),
                delegate { m_MerchantDatabase.AddInventoryItem(new InventoryItem(c_Item1, c_Category2, 1, 1)); },
                "Added item with its category not in the database");
            Assert.DoesNotThrow(
                delegate { m_MerchantDatabase.AddInventoryItem(new InventoryItem(c_Item2, c_Category1, 1, 1)); },
                "Item couldn't be added");
            Assert.AreEqual(2, m_MerchantDatabase.GetNumberOfInventoryItems(), "Number of inventory items doesn't match");
        }
    }
}
