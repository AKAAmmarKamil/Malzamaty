using Malzamaty.Model;
using Malzamaty.Repository;
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
            var service = (TheContext)validationContext.GetService(typeof(TheContext));
            var containerType = validationContext.ObjectInstance.GetType();
            var field = containerType.GetProperty("Authentication");
            var Property = field.GetValue(validationContext.ObjectInstance, null);
            var Role = service.Roles.FirstOrDefault(x => x.Id == (Guid)Property);
            var Class = new Class();
            var Subject = new Subject();
            var Message = new List<string>();
            if (validationContext.DisplayName == "Authentication")
            {
                if (Role == null)
                { return new ValidationResult("الصلاحية غير موجودة"); }
            }
            else if (validationContext.DisplayName == "ReWritePassword")
            {
                if (_comparisonProperty != value.ToString())
                {
                    return new ValidationResult("كلمتا السر غير متطابقتان");
                }
            }
            else if (validationContext.DisplayName == "Interests")
            {
                if (Role != null)
                {
                    var List = (List<Interests>)value;
                    for (int i = 0; i < List.Count; i++)
                    {
                        Class = service.Class.FirstOrDefault(x => x.ID == List[i].C_ID);
                        Subject = service.Subject.FirstOrDefault(x => x.ID == List[i].C_ID);
                        if (Class == null)
                        {
                            Message.Add("غير موجود"+" {"+List[i].C_ID+"} "+"الصف الذي يحمل المعرف");
                        }
                        if (Subject == null)
                        {
                            Message.Add("غير موجودة"+" {" + List[i].Su_ID+"} "+"المادة التي تحمل المعرف");
                        }
                        if (Message.Count > 0)
                        {
                            
                            return new ValidationResult(string.Join("\n", Message.ToArray()));
                        }
                    }

                    if (Role.ToString() == "Student")
                    {
                        if (List.Count > 1)
                        {
                            return new ValidationResult("لا يمكن للطالب إضافة أكثر من إهتمام");
                        }
                    } 
                }
            }
            return ValidationResult.Success;
        }
    }
}
