using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Malzamaty.Dto
{
    public class ScheduleWriteDto : IValidatableObject
    {
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public DateTime StartStudy { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public DateTime FinishStudy { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public Guid Subject { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var service = (MalzamatyContext)validationContext.GetService(typeof(MalzamatyContext));
            var File = service.Subject.FirstOrDefault(x => x.ID == Subject);
            if (File == null)
            {
                yield return new ValidationResult("المادة غير موجودة");
            }
            if (StartStudy<DateTime.Now|| FinishStudy < DateTime.Now)
            {
                yield return new ValidationResult("يجب أن يكون التاريخ أكبر من التاريخ الحالي");
            }
            if ((StartStudy-DateTime.Now).TotalDays>365|| (FinishStudy - DateTime.Now).TotalDays > 365)
            {
                yield return new ValidationResult("لا يمكن أن يكون التاريخ بعد سنة كاملة");
            }
            if (StartStudy>=FinishStudy)
            {
                yield return new ValidationResult("يجب أن يكون وقت إنهاء المادة أكبر من وقت البدء بدراستها");
            }
            yield return ValidationResult.Success;

        }
    }
}
