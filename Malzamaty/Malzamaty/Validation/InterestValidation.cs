using Malzamaty.Model;
using Malzamaty.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Validation
{
    public class InterestValidation :ValidationAttribute
    {
        private readonly string _classField;
        private readonly string _subjectField;

        public InterestValidation(string Class,string Subject)
        {
            _classField=Class;
            _subjectField = Subject;
        }

        protected override ValidationResult IsValid(object value,ValidationContext validationContext)
        {
            var service = (TheContext)validationContext.GetService(typeof(TheContext));
            var Subject = new Subject();
            var Role = new Roles();
            if (validationContext.DisplayName == "User")
            {
                var User = service.Users.FirstOrDefaultAsync(x => x.ID == (Guid)value);
                if(User.Result==null)
                { return new ValidationResult("المستخدم غير موجود"); }
                Role = service.Roles.FirstOrDefault(x => x.Id == User.Result.ID);
            }
            else if (validationContext.DisplayName == "Class")
            {
                var Class = service.Class.FirstOrDefaultAsync(x => x.ID == (Guid)value);
                if(Class.Result==null)
                { return new ValidationResult("الصف غير موجود"); }
            }
            else if(validationContext.DisplayName == "Subject")
            { 
            if (Role != new Roles())
                {
                    var ExistCheck=service.Matches.Where(x => x.Su_ID == Guid.Parse(_subjectField) && x.C_ID == Guid.Parse(_classField) && x.Su_ID == (Guid)value).ToListAsync();
                    if (ExistCheck.Result != null)
                    {
                        return new ValidationResult("الإهتمام موجود بالفعل");
                    }
                    var List = service.Matches.Where(x => x.C_ID ==Guid.Parse(_classField)&&x.Su_ID==(Guid)value).ToListAsync();
                    if (List.Result != null)
                    {
                        for (int i = 0; i < List.Result.Count; i++)
                        {
                            var Match = service.Matches.Where(x => x.C_ID == List.Result[i].C_ID && x.Su_ID == List.Result[i].Su_ID).FirstOrDefault();
                            var Class = service.Class.Include(x => x.Stage).Include(x => x.ClassType).FirstOrDefault(x => x.ID == List.Result[i].C_ID);
                            Subject = service.Subject.FirstOrDefault(x => x.ID == List.Result[i].Su_ID);

                            if (Match == null)
                            {
                                return new ValidationResult("مادة ال " + Subject.Name + " غير موجودة في الصف " + Class.Name + " " + Class.Stage.Name + " " + Class.ClassType.Name);
                            }
                            if (Class == null)
                            {
                                return new ValidationResult("غير موجود" + " {" + List.Result[i].C_ID + "} " + "الصف الذي يحمل المعرف");
                            }
                            if (Subject == null)
                            {
                                return new ValidationResult("غير موجودة" + " {" + List.Result[i].Su_ID + "} " + "المادة التي تحمل المعرف");
                            }
              
                        }
                    }
                    if (Role.ToString() == "Student")
                    {
                        if (List.Result.Count > 1)
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
