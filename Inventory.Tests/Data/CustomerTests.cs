using System;
using Inventory.Data;
using NUnit.Framework;

namespace Inventory.Tests.Data
{
    [TestFixture]
    public class CustomerTests
    {
        [Test]
        public void CreateCustomer()
        {
            const string c_CustomerName = "New Customer";
            const string c_CustomerPassword = "password";
            Customer customer = new Customer(c_CustomerName, c_CustomerPassword);
            Assert.AreEqual(c_CustomerName, customer.Name, "Customer name does not match");
            Assert.IsTrue(customer.HasPassword(c_CustomerPassword), "Customer password is not as expected");
        }

        [Test]
        public void CustomerNameNullOrEmptyThrows()
        {
            Assert.Throws(typeof (ArgumentException), delegate { new Customer(null, "password"); },
                "Exception is not thrown when customer name is null");
            Assert.Throws(typeof(ArgumentException), delegate { new Customer(string.Empty, "password"); },
                "Exception is not thrown when customer name is empty");
        }

        [Test]
        public void CustomerPasswordNullOrEmptyThrows()
        {
            Assert.Throws(typeof(ArgumentException), delegate { new Customer("Customer", null); },
                "Exception is not thrown when customer password is null");
            Assert.Throws(typeof(ArgumentException), delegate { new Customer("Customer", string.Empty); },
                "Exception is not thrown when customer password is empty");
        }
    }
}
