using System;
using Inventory.ServiceLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace Inventory.Tests
{
    [TestFixture]
    public class ServiceTests
    {
        private MerchantDatabase m_MerchantDatabase;

        [TestFixtureSetUp]
        public void Setup()
        {
            m_MerchantDatabase = new MerchantDatabase();
        }

        [TearDown]
        public void TearDown()
        {
            m_MerchantDatabase = new MerchantDatabase();    
        }

        [Test]
        public void TestMethod1()
        {
        }
    }
}
