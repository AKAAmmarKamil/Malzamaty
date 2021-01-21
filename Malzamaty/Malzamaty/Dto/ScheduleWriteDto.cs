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
        public DateTime? StartStudy { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public DateTime? FinishStudy { get; set; }
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
            yield return ValidationResult.Success;

        }
    }
}
