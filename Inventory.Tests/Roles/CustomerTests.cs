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
    }
}
