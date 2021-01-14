using Malzamaty.Model;
using Malzamaty.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
            var Subject = new Subject();
            var Role = new Roles();
            var Message = new List<string>();
            if (validationContext.DisplayName == "Authentication")
            {
                Role = service.Roles.FirstOrDefault(x => x.Id == (Guid)value);

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
                if (Role != new Roles())
                {
                 
                    var List = (List<Interests>)value;
                    for (int i = 0; i < List.Count; i++)
                    {
                        var Match = service.Matches.Where(x => x.C_ID == List[i].C_ID&&x.Su_ID==List[i].Su_ID).FirstOrDefault();
                        var  Class = service.Class.Include(x => x.Stage).Include(x => x.ClassType).FirstOrDefault(x => x.ID == List[i].C_ID);
                        Subject = service.Subject.FirstOrDefault(x => x.ID == List[i].Su_ID);

                        if (Match == null)
                                {
                                    Message.Add("مادة ال "+Subject.Name+" غير موجودة في الصف "+Class.Name+" "+Class.Stage.Name+" "+Class.ClassType.Name);
                                }
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
