using Malzamaty.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace Malzamaty.Dto
{
    public class InterestWriteDto : IValidatableObject
    {

        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public Guid User { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public Guid Class { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public Guid Subject { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var service = (MalzamatyContext)validationContext.GetService(typeof(MalzamatyContext));
            var Subject = new Subject();
            var User = service.Users.FirstOrDefaultAsync(x => x.ID == this.User);
            if (User.Result == null)
            { yield return new ValidationResult("المستخدم غير موجود"); }
            var Role = service.Roles.FirstOrDefault(x => x.Id == User.Result.Authentication);
            var Class = service.Class.Include(x => x.Stage).Include(x => x.ClassType).FirstOrDefault(x => x.ID == this.Class);
            if (Class == null)
            { yield return new ValidationResult("الصف غير موجود"); }
            if (Role != null)
            {
                var ExistCheck = service.Interests.Where(x => x.U_ID == this.User && x.C_ID == this.Class && x.Su_ID == this.Subject).ToListAsync();
                if (ExistCheck.Result.Count() > 0)
                {
                    yield return new ValidationResult("الإهتمام موجود بالفعل");
                }
                var List = service.Matches.Where(x => x.C_ID == this.Class && x.Su_ID == this.Subject).ToListAsync();
                if (List.Result.Count > 0)
                {
                    for (int i = 0; i < List.Result.Count; i++)
                    {
                        var Match = service.Matches.Where(x => x.C_ID == List.Result[i].C_ID && x.Su_ID == List.Result[i].Su_ID).FirstOrDefault();
                        Subject = service.Subject.FirstOrDefault(x => x.ID == List.Result[i].Su_ID);

                        if (Match == null)
                        {
                            yield return new ValidationResult("مادة ال " + Subject.Name + " غير موجودة في الصف " + Class.Name + " " + Class.Stage.Name + " " + Class.ClassType.Name);
                        }
                        if (Class == null)
                        {
                            yield return new ValidationResult("غير موجود" + " {" + List.Result[i].C_ID + "} " + "الصف الذي يحمل المعرف");
                        }
                        if (Subject == null)
                        {
                            yield return new ValidationResult("غير موجودة" + " {" + List.Result[i].Su_ID + "} " + "المادة التي تحمل المعرف");
                        }
                    }
                }
            }
        }
    }
}