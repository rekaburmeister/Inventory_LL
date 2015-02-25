using System;
using Inventory.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inventory.Tests.Data
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void CreateCustomer()
        {
            const string c_CustomerName = "New Customer";
            const string c_CustomerPassword = "password";
            Customer customer = new Customer(c_CustomerName, c_CustomerPassword);
            Assert.AreEqual(c_CustomerName, customer.Name, "Customer name does not match");
            Assert.IsTrue(customer.HasPassword(c_CustomerPassword), "Customer password is not as expected");
        }
    }
}
