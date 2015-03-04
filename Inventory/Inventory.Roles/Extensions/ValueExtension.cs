using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Roles.Extensions
{
    public static class ValueExtension
    {
        public static ValueType EnsureGreaterThanZero(this ValueType value)
        {
            if (Convert.ToInt16(value) <= 0)
            {
                throw new ArgumentException("Value has to be greater than 0!");
            }
            return value;
        }
    }
}
