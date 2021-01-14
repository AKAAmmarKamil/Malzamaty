using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Validation
{
    public class UserValidation :ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public UserValidation(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }
        protected override ValidationResult IsValid(object value,ValidationContext validationContext)
        {
            var x= validationContext.DisplayName;
            if (value.GetType().Name != "String")
            {
                var List = (List<Interests>)value;
                if (List.Count > 1)
                {
                    return new ValidationResult("لا يمكن للطالب إضافة أكثر من إهتمام");
                }
            }
            if (_comparisonProperty != value.ToString())
            {
                return new ValidationResult("كلمتا السر غير متطابقتان");
            }
            
            return ValidationResult.Success;
        }
    }
}
