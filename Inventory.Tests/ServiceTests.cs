using System;
using System.Linq;
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
    }
}
