using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Malzamaty.Dto
{
    public class ReportWriteDto : IValidatableObject
    {
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public string Description { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public Guid F_ID { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //var service = (MalzamatyContext)validationContext.GetService(typeof(MalzamatyContext));
            //var File = service.File.FirstOrDefault(/*x => x.ID == F_ID*/);
            //if (File == null)
            //{
            //    yield return new ValidationResult("الملف غير موجود");
            //}
            yield return ValidationResult.Success;

        }
    }
}
