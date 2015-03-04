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

        [Test]
        public void PlaceOrder()
        {
            Supplier supplier = new Supplier("Supplier 1");
            var item1 = new InventoryItem("item 1", "category 1", 20.2, 5);
            var item2 = new InventoryItem("item 2", "category 1", 21.2, 4);
            var item3 = new InventoryItem("item 3", "category 1", 22.2, 3);
            var item4 = new InventoryItem("item 4", "category 1", 23.2, 2);
            var item5 = new InventoryItem("item 5", "category 1", 24.2, 1);
            supplier.AddToInventory(item1);
            supplier.AddToInventory(item2);
            supplier.AddToInventory(item3);
            supplier.AddToInventory(item4);
            supplier.AddToInventory(item5);

            Order order = new Order
            {
                Goods =
                    new List<InventoryItem>
                    {
                        new InventoryItem("item 2", "category 1", 21.2, 2),
                        new InventoryItem("item 4", "category 1", 23.2, 1)
                    }
            };

            supplier.PlaceOrder(order);

            Assert.AreEqual(5, supplier.GetStock(item1), "Item 1 stock ok");
            Assert.AreEqual(2, supplier.GetStock(item2), "Item 2 stock ok");
            Assert.AreEqual(3, supplier.GetStock(item3), "Item 3 stock ok");
            Assert.AreEqual(1, supplier.GetStock(item4), "Item 4 stock ok");
            Assert.AreEqual(1, supplier.GetStock(item5), "Item 5 stock ok");
        }
    }
}
