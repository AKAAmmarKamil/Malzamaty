using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Malzamaty.Dto
{
    public class RatingWriteDto : IValidatableObject
    {
        public string Comment { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public int Rate { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public Guid FileID { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var service = (MalzamatyContext)validationContext.GetService(typeof(MalzamatyContext));
            var File = service.File.FirstOrDefault(x => x.ID == FileID);
            if (File == null)
            {
                yield return new ValidationResult("الملف غير موجود");
            }
            yield return ValidationResult.Success;

        }
    }
}
