using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace eCommerce.Code.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class PositiveNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var inputValue = (string)value;
            if(string.IsNullOrEmpty(inputValue))
            {
                return false;
            }

            if(!inputValue.All(Char.IsDigit))
            {
                return false;
            }

            return true;
        }
    }
}
