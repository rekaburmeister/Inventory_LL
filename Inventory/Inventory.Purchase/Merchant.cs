using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Data;
using Inventory.ServiceLayer;

namespace Inventory.Purchase
{
    public class Merchant
    {
        private MerchantDatabase Database { get; set; }

        public Merchant()
        {
            Database = new MerchantDatabase();
        }

        public void AddCustomer(LiveCustomer liveCustomer)
        {
            Customer liveCustomerDbObject = new Customer(liveCustomer.UserName, liveCustomer.Password);
            Database.AddCustomer(liveCustomerDbObject);
        }

        public bool AuthenticateUser(string livecustomer, string password)
        {
            Customer customer;
            try
            {
                customer = Database.FindCustomer(livecustomer);
            }
            catch (InvalidOperationException e)
            {
                return false;
            }
            return customer.HasPassword(password);
        }
    }
}
