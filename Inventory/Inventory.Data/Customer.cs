using System;

namespace Inventory.Data
{
    public class Customer
    {
        public string Name { get; private set; }
        private string Password { get; set; }

        public Customer(string name, string password)
        {
            
            Name = name;
            Password = password;
        }

        public bool HasPassword(string customerPassword)
        {
            if (string.IsNullOrEmpty(customerPassword))
            {
                return false;
            }
            return Password.Equals(customerPassword);
        }
    }
}
