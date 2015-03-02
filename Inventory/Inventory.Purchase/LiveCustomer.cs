using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Purchase
{
    public class LiveCustomer
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public LiveCustomer(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
