using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Malzamaty.Dto
{
    public class ScheduleWriteDto : IValidatableObject
    {
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public DateTimeOffset StartStudy { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public DateTimeOffset FinishStudy { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public Guid Subject { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var service = (MalzamatyContext)validationContext.GetService(typeof(MalzamatyContext));
            var subject = service.Subject.FirstOrDefault(x => x.ID == Subject);
            if (subject == null)
            {
                yield return new ValidationResult("المادة غير موجودة");
            }
            var Now = DateTimeOffset.Now;
            if (StartStudy < Now || FinishStudy < Now)
            {
                yield return new ValidationResult("يجب أن يكون التاريخ أكبر من التاريخ الحالي");
            }
            if ((StartStudy - Now).TotalDays > 365 || (FinishStudy - Now).TotalDays > 365)
            {
                yield return new ValidationResult("لا يمكن أن يكون التاريخ بعد سنة كاملة");
            }
            if (StartStudy >= FinishStudy)
            {
                yield return new ValidationResult("يجب أن يكون وقت إنهاء المادة أكبر من وقت البدء بدراستها");
            }
            yield return ValidationResult.Success;

        }
    }
}
