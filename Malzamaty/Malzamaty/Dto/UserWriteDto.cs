using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Malzamaty.Dto
{

    public class UserWriteDto : IValidatableObject
    {
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        [EmailAddress(ErrorMessage = "البريد الألكتروني غير صحيح")]
        public string Email { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        [MinLength(8,ErrorMessage ="كلمة السر يجب ان تكون 8 أحرف كحد أدنى")]
        public string Password { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        [Compare(nameof(Password), ErrorMessage = "كلمتا السر غير متطابقتان")]
        public string ReWritePassword { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public Guid Authentication { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public List<Interests> Interests { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var service = (MalzamatyContext)validationContext.GetService(typeof(MalzamatyContext));
            var Subject = new Subject();
            var EmailAddress = service.Users.FirstOrDefault(x => x.Email == Email);
            if (EmailAddress != null)
            {
                yield return new ValidationResult("البريد الألكتروني غير صحيح");
            }
            var Role = service.Roles.FirstOrDefault(x => x.Id == Authentication);
            if (Role == null)
            {
                yield return new ValidationResult("الصلاحية غير موجودة");
            }
            if (Role != null)
            {
                for (int i = 0; i < Interests.Count; i++)
                {
                    var Match = service.Matches.Where(x => x.C_ID == Interests[i].C_ID && x.Su_ID == Interests[i].Su_ID).FirstOrDefault();
                    var Class = service.Class.Include(x => x.Stage).Include(x => x.ClassType).FirstOrDefault(x => x.ID == Interests[i].C_ID);
                    Subject = service.Subject.FirstOrDefault(x => x.ID == Interests[i].Su_ID);

                    if (Match == null && Class != null && Subject != null)
                    {
                        yield return new ValidationResult("مادة ال " + Subject.Name + " غير موجودة في الصف " + Class.Name + " " + Class.Stage.Name + " " + Class.ClassType.Name);
                    }
                    if (Class == null)
                    {
                        yield return new ValidationResult("غير موجود" + " {" + Interests[i].C_ID + "} " + "الصف الذي يحمل المعرف");
                    }
                    if (Subject == null)
                    {
                        yield return new ValidationResult("غير موجودة" + " {" + Interests[i].Su_ID + "} " + "المادة التي تحمل المعرف");
                    }

                }
                if (Role.Role.ToString() == "Student")
                {
                    if (Interests.Count > 1)
                    {
                        yield return new ValidationResult("لا يمكن للطالب إضافة أكثر من إهتمام");
                    }
                }
            }

            yield return ValidationResult.Success;
        }
    }
}