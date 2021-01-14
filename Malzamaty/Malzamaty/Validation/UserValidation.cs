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
            if (validationContext.DisplayName == "ReWritePassword")
            {
                if (_comparisonProperty != value.ToString())
                {
                    return new ValidationResult("كلمتا السر غير متطابقتان");
                }
            }
            else
            {
                var Role = validationContext.ObjectInstance;
                var List = (List<Interests>)value;
                if (List.Count > 1)
                {
                    return new ValidationResult("لا يمكن للطالب إضافة أكثر من إهتمام");
                }
            }
            return ValidationResult.Success;
        }
    }
}
