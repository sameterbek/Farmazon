using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Farmazon.Util.Validation
{
    public class GenericValidator
    {
        public static bool TryValidate(object obj, out ICollection<ValidationResult> results)
        {
            var context = new ValidationContext(obj, serviceProvider: null, items: null);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(
                obj, context, results,
                validateAllProperties: true
            );
        }
    }
}
