using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inventory.Tests.Data
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void CreateCustomer()
        {
            const string customerName = "New Customer";
            const string customerPassword = "password";
            Customer customer = new Customer(customerName, customerPassword);
            Assert.AreEqual(customerName, customer.Name, "Customer name does not match");
            Assert.IsTrue(customer.HasPassword(customerPassword), "Customer password is not as expected");
        }
    }
}
