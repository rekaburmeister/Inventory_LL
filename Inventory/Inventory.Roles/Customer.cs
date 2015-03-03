using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Roles
{
    public class Customer
    {
        public Customer(string firstName, string surname, string userName, string password)
        {
            FirstName = firstName;
            Surname = surname;
            UserName = userName;
            Password = password;
            Balance = 0.0;
        }

        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public double Balance { get; private set; }
    }
}
