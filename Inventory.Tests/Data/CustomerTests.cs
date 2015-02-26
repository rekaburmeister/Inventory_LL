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
            Assert.AreEqual(c_CustomerName, customer.UserName, "Customer name does not match");
            Assert.IsTrue(customer.HasPassword(c_CustomerPassword), "Customer password is not as expected");
        }

        [TestCase(null, TestName = "Null")]
        [TestCase("", TestName = "Empty")] // wouldn't let  me do string.Empty here, value must be constant
        public void CustomerNameNullOrEmptyThrows(string userName)
        {
            Assert.Throws(typeof (ArgumentException), delegate { new Customer(userName, "password"); });
        }

        [TestCase(null, TestName="Null")]
        [TestCase("", TestName = "Empty")]
        public void CustomerPasswordNullOrEmptyThrows(string password)
        {
            Assert.Throws(typeof(ArgumentException), delegate { new Customer("Customer", password); });
        }
    }
}
