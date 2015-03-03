using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Roles.Extensions;

namespace Inventory.Roles
{
    public class Customer
    {
        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public double Balance { get; private set; }
        
        public Customer(string firstName, string surname, string userName, string password)
        {
            FirstName = firstName.ValidateInput();
            Surname = surname.ValidateInput();
            UserName = userName.ValidateInput();
            Password = password.ValidateInput();
            Balance = 0.0;
        }
    }
}
