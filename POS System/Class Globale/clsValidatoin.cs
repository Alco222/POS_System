using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DVLD.Classes
{
    public class clsValidatoin
    {

        public static bool ValidateEmail(string emailAddress)
        {
            var pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

            var regex = new Regex(pattern);

            return regex.IsMatch(emailAddress);
        }

        public static bool ValidateInteger(string Number)
        {
            var pattern = @"^[0-9]*$";

            var regex = new Regex(pattern);

            return regex.IsMatch(Number);
        }

        public static bool ValidateFloat(string Number)
        {
            var pattern = @"^[0-9]*(?:\.[0-9]*)?$";

            var regex = new Regex(pattern);

            return regex.IsMatch(Number);
        }

        public static bool IsNumber(string Number)
        {
            return (ValidateInteger(Number) || ValidateFloat(Number));
        }

        /// <summary>
        /// Validate a single property of a model using DataAnnotations.
        /// </summary>
        public static bool ValidateProperty<T>(T instance, string propertyName, out string errorMessage)
        {
            errorMessage = string.Empty;

            var property = typeof(T).GetProperty(propertyName);
            if (property == null)
                return true; // Property not found → assume valid

            var value = property.GetValue(instance);

            var context = new ValidationContext(instance)
            {
                MemberName = propertyName
            };

            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateProperty(value, context, results);

            if (!isValid)
                errorMessage = results.First().ErrorMessage;

            return isValid;
        }
    
    }
}
