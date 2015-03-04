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
    class SupplierTests
    {
        [TestCase("")]
        [TestCase(null)]
        public void NameNullOrEmpty(string name)
        {
            Assert.Throws(typeof(ArgumentException), () => { new Supplier(name); });
        }
        
        [Test]
        public void TwoSuppliersWithSameName()
        {
            Supplier supplier1 = new Supplier("Supplier 1");
            Supplier supplier2 = new Supplier("Supplier 1");

            Assert.AreNotEqual(supplier1, supplier2, "Suppliers are the same");
        }
    }
}
