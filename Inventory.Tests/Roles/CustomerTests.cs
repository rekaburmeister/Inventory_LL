using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Roles;
using NUnit.Framework;

namespace Inventory.Tests.Roles
{
    [TestFixture]
    class CustomerTests
    {
        [TestCase("John", "Doe", "john.doe", "JohnDoe11")]
        public void CreateCustomer(string firstName, string surname, string userName, string password)
        {
            var customer = new Customer(firstName, surname, userName, password);
            Assert.AreEqual(firstName, customer.FirstName, "First name was set correctly");
            Assert.AreEqual(surname, customer.Surname, "Surname was set correctly");
            Assert.AreEqual(userName, customer.UserName, "UserName was set correctly");
            Assert.AreEqual(password, customer.Password, "Password was set correctly");
            Assert.AreEqual(0.0, customer.Balance, "Default balance is 0");
        }

        [TestCase("", "Doe", "john.doe", "JohnDoe11")]
        [TestCase("John", "", "john.doe", "JohnDoe11")]
        [TestCase("John", "Doe", "", "JohnDoe11")]
        [TestCase("John", "Doe", "john.doe", "")]
        [TestCase(null, "Doe", "john.doe", "JohnDoe11")]
        [TestCase("John", null, "john.doe", "JohnDoe11")]
        [TestCase("John", "Doe", null, "JohnDoe11")]
        [TestCase("John", "Doe", "john.doe", null)]
        public void InvalidArgumentHandling(string firstName, string surname, string userName, string password)
        {
            Assert.Throws(typeof(ArgumentException), () => { new Customer(firstName, surname, userName, password); });
        }
    }
}
