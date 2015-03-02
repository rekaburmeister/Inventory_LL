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
        private LiveSupplier m_LiveSupplier;
        private LiveSupplier m_FailoverSupplier;
        private LiveSupplier m_SecondaryFailoverSupplier;

        private const string c_Customer1 = "LiveCustomer 1";
        private const string c_Password1 = "Password1";

        [TestFixtureSetUp]
        public void Setup()
        {
            m_Merchant = new Merchant();
            var customer1 = new LiveCustomer(c_Customer1, c_Password1);

            m_Merchant.AddCustomer(customer1);

            m_LiveSupplier = new LiveSupplier("LiveSupplier 1");
            var supplier2 = new LiveSupplier("LiveSupplier 2");
            m_FailoverSupplier = new LiveSupplier("LiveSupplier 3");
            var supplier4 = new LiveSupplier("LiveSupplier 4");
            m_SecondaryFailoverSupplier = new LiveSupplier("LiveSupplier 5");

            var categories = new [] {"Cat 1", "Cat 2", "Cat 3"};

            foreach (string category in categories)
            {
                m_LiveSupplier.AddCategory(category);
                supplier2.AddCategory(category);
                m_FailoverSupplier.AddCategory(category);
                supplier4.AddCategory(category);
                m_SecondaryFailoverSupplier.AddCategory(category);
            }

            m_LiveSupplier.AddItem(new SupplierItem("Item 1", "Cat 1", 300, 5));
            m_LiveSupplier.AddItem(new SupplierItem("Item 2", "Cat 1", 300, 3));
            m_LiveSupplier.AddItem(new SupplierItem("Item 3", "Cat 2", 300, 2));
            m_LiveSupplier.AddItem(new SupplierItem("Item 5", "Cat 3", 300, 3));

            supplier2.AddItem(new SupplierItem("Item 4", "Cat 2", 300, 1));
            supplier2.AddItem(new SupplierItem("Item 5", "Cat 3", 370, 3));
            supplier2.AddItem(new SupplierItem("Item 6", "Cat 3", 300, 4));

            m_FailoverSupplier.AddItem(new SupplierItem("Item 2", "Cat 1", 300, 3));
            m_FailoverSupplier.AddItem(new SupplierItem("Item 3", "Cat 2", 300, 1));

            supplier4.AddItem(new SupplierItem("Item 5", "Cat 3", 300, 3));
            supplier4.AddItem(new SupplierItem("Item 2", "Cat 1", 300, 3));

            m_SecondaryFailoverSupplier.AddItem(new SupplierItem("Item 2", "Cat 1", 300, 3));
            m_SecondaryFailoverSupplier.AddItem(new SupplierItem("Item 3", "Cat 2", 350, 2));

            m_Merchant.InitiliseSuppliers(new[] { m_LiveSupplier, supplier2, m_FailoverSupplier, supplier4, m_SecondaryFailoverSupplier});
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
            int supplierStock = m_LiveSupplier.StockOf("Item 1", "Cat 1");
            m_Merchant.BuyItem("Item 1", "Cat 1");
            Assert.AreEqual(supplierStock - 1, m_LiveSupplier.StockOf("Item 1", "Cat 1"), "Supplier stock didn't decrease with purchase");
        }

        [Test]
        public void FailoverSupplier()
        {
            const string c_ItemName = "Item 2";
            const string c_CategoryName = "Cat 1";
            Assert.AreEqual(3, m_LiveSupplier.StockOf(c_ItemName, c_CategoryName), "Initial stock for item is not 3");
            Assert.AreEqual(3, m_FailoverSupplier.StockOf(c_ItemName, c_CategoryName), "Initial stock for item is not 3");
            m_Merchant.BuyItem(c_ItemName, c_CategoryName);
            m_Merchant.BuyItem(c_ItemName, c_CategoryName);
            m_Merchant.BuyItem(c_ItemName, c_CategoryName);

            Assert.AreEqual(0, m_LiveSupplier.StockOf(c_ItemName, c_CategoryName), "Stock didn't go down to 0");

            m_Merchant.BuyItem(c_ItemName, c_CategoryName);
            m_Merchant.BuyItem(c_ItemName, c_CategoryName);

            Assert.AreEqual(1, m_FailoverSupplier.StockOf("Item 2", "Cat 1"), "Stock didn't go down to 1");
            
        }

        [Test]
        public void FailoverToNthSupplier()
        {
            const string c_ItemName = "Item 3";
            const string c_CategoryName = "Cat 2";
            Assert.AreEqual(2, m_LiveSupplier.StockOf(c_ItemName, c_CategoryName), "Initial stock for item is not 2");
            Assert.AreEqual(1, m_FailoverSupplier.StockOf(c_ItemName, c_CategoryName), "Initial stock for item is not 1");
            Assert.AreEqual(2, m_SecondaryFailoverSupplier.StockOf(c_ItemName, c_CategoryName), "Initial stock for item is not 2");

            m_Merchant.BuyItem(c_ItemName, c_CategoryName);
            Assert.AreEqual(1, m_LiveSupplier.StockOf(c_ItemName, c_CategoryName), "Stock didn't change");
            Assert.AreEqual(1, m_FailoverSupplier.StockOf(c_ItemName, c_CategoryName), "Stock changed");
            Assert.AreEqual(2, m_SecondaryFailoverSupplier.StockOf(c_ItemName, c_CategoryName), "Stock changed");
            
            m_Merchant.BuyItem(c_ItemName, c_CategoryName);
            Assert.AreEqual(0, m_LiveSupplier.StockOf(c_ItemName, c_CategoryName), "Stock didn't change");
            Assert.AreEqual(1, m_FailoverSupplier.StockOf(c_ItemName, c_CategoryName), "Stock changed");
            Assert.AreEqual(2, m_SecondaryFailoverSupplier.StockOf(c_ItemName, c_CategoryName), "Stock changed");

            m_Merchant.BuyItem(c_ItemName, c_CategoryName);
            Assert.AreEqual(0, m_LiveSupplier.StockOf(c_ItemName, c_CategoryName), "Stock changed");
            Assert.AreEqual(0, m_FailoverSupplier.StockOf(c_ItemName, c_CategoryName), "Stock didn't change");
            Assert.AreEqual(2, m_SecondaryFailoverSupplier.StockOf(c_ItemName, c_CategoryName), "Stock changed");

            m_Merchant.BuyItem(c_ItemName, c_CategoryName);
            Assert.AreEqual(0, m_LiveSupplier.StockOf(c_ItemName, c_CategoryName), "Stock changed");
            Assert.AreEqual(0, m_FailoverSupplier.StockOf(c_ItemName, c_CategoryName), "Stock changed");
            Assert.AreEqual(1, m_SecondaryFailoverSupplier.StockOf(c_ItemName, c_CategoryName), "Stock didn't change");
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
