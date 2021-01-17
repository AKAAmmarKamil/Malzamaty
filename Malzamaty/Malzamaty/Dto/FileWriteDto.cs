using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Dto
{
    public class FileWriteDto : ValidationAttribute
    {
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public string Description { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public string FilePath { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public int PublishDate { get; set; }
        public Guid Class { get; set; }
        public Guid User { get; set; }
        public Guid Subject { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Enumerable.Range(1900, DateTime.Now.Year).Contains(PublishDate))
                yield return new ValidationResult("سنة النشر غير صحيحة");

        }
    }
}
   