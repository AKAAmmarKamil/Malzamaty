using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Malzamaty.Dto
{
    public class FileWriteDto : IValidatableObject
    {
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public string Description { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public string FilePath { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public int PublishDate { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public Guid Subject { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public Guid Class { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Enumerable.Range(1900, DateTime.Now.Year).Contains(PublishDate))
                yield return new ValidationResult("سنة النشر غير صحيحة");
            var service = (MalzamatyContext)validationContext.GetService(typeof(MalzamatyContext));
            var Class = service.Class.Include(x => x.Stage).Include(x => x.ClassType).FirstOrDefault(x => x.ID == this.Class);
            if (Class == null)
            {
                yield return new ValidationResult("الصف غير موجود");
            }
            var Subject = service.Subject.FirstOrDefault(x => x.ID == this.Subject);
            if (Subject == null)
            {
                yield return new ValidationResult("المادة غير موجودة");
            }
            var Matches = service.Matches.Where(x => x.ClassID == this.Class && x.SubjectID == this.Subject).FirstOrDefaultAsync();
            if (Matches.Result == null && Class != null && Subject != null)
            {
                yield return new ValidationResult("مادة ال " + Subject.Name + " غير موجودة في الصف " + Class.Name + " " + Class.Stage.Name + " " + Class.ClassType.Name);
            }
        }
    }
}