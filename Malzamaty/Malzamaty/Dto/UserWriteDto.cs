using Malzamaty.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        [MinLength(8, ErrorMessage = "كلمة السر يجب ان تكون 8 أحرف كحد أدنى")]
        public string Password { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        [Compare(nameof(Password), ErrorMessage = "كلمتا السر غير متطابقتان")]
        public string ReWritePassword { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public string Role { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public List<Interests> Interests { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public AddressWriteDto Address { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var service = (MalzamatyContext)validationContext.GetService(typeof(MalzamatyContext));
            var Subject = new Subject();
            var EmailAddress = service.Users.FirstOrDefault(x => x.Email == Email);
            if (EmailAddress != null)
            {
                yield return new ValidationResult("البريد الألكتروني غير صحيح");
            }
            //var Address = service.Address.FirstOrDefault(x => x.Id ==this.Address);
            //if (Address == null)
            //{
            //    yield return new ValidationResult("العنوان غير موجود");
            //}
            if (Role != null)
            {
                for (int i = 0; i < Interests.Count; i++)
                {
                    var Match = service.Matches.Where(x => x.ClassID == Interests[i].ClassID && x.SubjectID == Interests[i].SubjectID).FirstOrDefault();
                    var Class = service.Class.Include(x => x.Stage).Include(x => x.ClassType).FirstOrDefault(x => x.ID == Interests[i].ClassID);
                    Subject = service.Subject.FirstOrDefault(x => x.ID == Interests[i].SubjectID);

                    if (Match == null && Class != null && Subject != null)
                    {
                        yield return new ValidationResult("مادة ال " + Subject.Name + " غير موجودة في الصف " + Class.Name + " " + Class.Stage.Name + " " + Class.ClassType.Name);
                    }
                    if (Class == null)
                    {
                        yield return new ValidationResult("غير موجود" + " {" + Interests[i].ClassID + "} " + "الصف الذي يحمل المعرف");
                    }
                    if (Subject == null)
                    {
                        yield return new ValidationResult("غير موجودة" + " {" + Interests[i].SubjectID + "} " + "المادة التي تحمل المعرف");
                    }

                }
                if (Role == "Student")
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