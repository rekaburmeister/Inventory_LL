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
                delegate { m_MerchantDatabase.AddItem(new MerchantItem(c_Item1, c_Category1, 1, 1)); },
                "Item couldn't be added");
            Assert.Throws(
                typeof (Exception),
                delegate { m_MerchantDatabase.AddItem(new MerchantItem(c_Item1, c_Category2, 1, 1)); },
                "Added item with its category not in the database");
            Assert.DoesNotThrow(
                delegate { m_MerchantDatabase.AddItem(new MerchantItem(c_Item2, c_Category1, 1, 1)); },
                "Item couldn't be added");
            Assert.AreEqual(2, m_MerchantDatabase.GetNumberOfInventoryItems(), "Number of inventory items doesn't match");
        }

        [Test]
        public void AddAndGetCustomer()
        {
            const string c_UserName = "customer";
            Customer customer = new Customer(c_UserName, "password");
            Assert.Throws(
                typeof(InvalidOperationException), 
                delegate { m_MerchantDatabase.FindCustomer(c_UserName); },
                "No exception raised when customer wasn't found");
            m_MerchantDatabase.AddCustomer(customer);
            Customer returned = m_MerchantDatabase.FindCustomer(c_UserName);
            Assert.AreEqual(customer, returned, "Original and loaded customer are not the same");
        }

        [Test]
        public void CantCreateCustomerWithSameUserName()
        {
            const string c_UserName = "customer";
            Customer customer1 = new Customer(c_UserName, "password");
            Customer customer2 = new Customer(c_UserName, "password2");

            m_MerchantDatabase.AddCustomer(customer1);

            Assert.Throws(
                typeof(Exception),
                delegate { m_MerchantDatabase.AddCustomer(customer2); },
                "No exception raised when customer is already in the database");
        }

        [Test]
        public void CantCreateCustomerWithSamePassword()
        {
            const string c_Password = "password";
            Customer customer1 = new Customer("user1", c_Password);
            Customer customer2 = new Customer("user2", c_Password);

            m_MerchantDatabase.AddCustomer(customer1);

            Assert.DoesNotThrow(
                delegate { m_MerchantDatabase.AddCustomer(customer2); },
                "Couldn't add customer with same password");
        }

        [Test]
        public void AddAndGetSupplier()
        {
            const string c_SupplierName = "supplier";
            Supplier supplier = new Supplier(c_SupplierName);
            Assert.Throws(
                typeof(InvalidOperationException),
                delegate { m_MerchantDatabase.FindSupplier(c_SupplierName); },
                "No exception raised when Supplier wasn't found");
            m_MerchantDatabase.AddSupplier(supplier);
            Supplier returned = m_MerchantDatabase.FindSupplier(c_SupplierName);
            Assert.AreEqual(supplier, returned, "Original and loaded Supplier are not the same");
        }

        [Test]
        public void CantCreateSameSupplierTwice()
        {
            const string c_SupplierName = "Supplier";
            Supplier supplier1 = new Supplier(c_SupplierName);
            Supplier supplier2 = new Supplier(c_SupplierName);

            m_MerchantDatabase.AddSupplier(supplier1);

            Assert.Throws(
                typeof(Exception),
                delegate { m_MerchantDatabase.AddSupplier(supplier2); },
                "No exception raised when Supplier is already in the database");
        }
    }
}
