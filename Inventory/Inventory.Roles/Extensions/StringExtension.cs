using System;

namespace Inventory.Roles.Extensions
{
    public static class StringExtension
    {
        public static string ValidateInput(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Argument can't be null or empty");
            }
            return input;
        } 
    }
}
