using System;
using System.Collections.Generic;
using Inventory.Data;
using Inventory.ServiceLayer;
using NUnit.Framework;
using Inventory.Purchase;

namespace Inventory.Tests
{
    [TestFixture]
    public class PurchaseTests
    {
        private Merchant m_Merchant;
        private List<LiveCustomer> m_Customers;
        private List<LiveSupplier> m_Suppliers;

        private const string c_Customer1 = "LiveCustomer 1";
        private const string c_Password1 = "Password1";

        [TestFixtureSetUp]
        public void Setup()
        {
            m_Merchant = new Merchant();
            var customer1 = new LiveCustomer(c_Customer1, c_Password1);

            m_Merchant.AddCustomer(customer1);

            var supplier1 = new LiveSupplier("LiveSupplier 1");
            var supplier2 = new LiveSupplier("LiveSupplier 2");
            var supplier3 = new LiveSupplier("LiveSupplier 3");
            var supplier4 = new LiveSupplier("LiveSupplier 4");
            var supplier5 = new LiveSupplier("LiveSupplier 5");

            var categories = new [] {"Cat 1", "Cat 2", "Cat 3"};
            var item1 = new InventoryItem("Item 1", "Cat 1", 300, 5);
            var item2 = new InventoryItem("Item 2", "Cat 1", 300, 3);
            var item3 = new InventoryItem("Item 3", "Cat 2", 300, 2);
            var item4 = new InventoryItem("Item 4", "Cat 2", 300, 1);
            var item5 = new InventoryItem("Item 5", "Cat 3", 300, 3);
            var item6 = new InventoryItem("Item 5", "Cat 3", 370, 3);
            var item7 = new InventoryItem("Item 5", "Cat 3", 350, 2);
            var item8 = new InventoryItem("Item 6", "Cat 3", 300, 4);
        }

        [Test]
        public void CustomerLogin()
        {
            Assert.IsTrue(m_Merchant.AuthenticateUser(c_Customer1, c_Password1), "Valid credentials couldn't log in");
            Assert.IsFalse(m_Merchant.AuthenticateUser(c_Customer1, "WrongPassword"), "Invalid credentials logged in");
            Assert.IsFalse(m_Merchant.AuthenticateUser("WrongCustomer", "WrongPassword"), "Invalid credentials logged in");
            Assert.IsFalse(m_Merchant.AuthenticateUser("WrongCustomer", c_Password1), "Invalid credentials logged in");
        }

        [Test]
        public void PurchaseItem()
        {
            Assert.IsTrue(m_Merchant.BuyItem("Item 1", "Cat 1"), "Couldn't purchase the item");
        }

        [Test]
        public void UpdateSupplierStock()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void FailoverSupplier()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void SecondaryFailoverSupplier()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CreateInvoice()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void ProfitAndLoss()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CountOfStockPerItem()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void ListOfItemsPerCategories()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void ListOfItemsFromSupplier()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void ListOfSuppliersOfItem()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void OutOfStockWarning()
        {
            throw new NotImplementedException();
        }
    }
}
