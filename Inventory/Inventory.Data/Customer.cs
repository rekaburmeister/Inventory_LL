using System;

namespace Inventory.Data
{
    public class Customer
    {
        public string UserName { get; private set; }
        private string Password { get; set; }

        public Customer(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Name can't be null or empty");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password can't be null or empty");
            }

            UserName = userName;
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
