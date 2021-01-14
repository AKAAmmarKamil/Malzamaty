using Malzamaty.Validation;
using System;
using System.ComponentModel.DataAnnotations;
namespace Malzamaty.Dto
{
    public class InterestWriteDto
    {
        [Required(ErrorMessage ="لا يمكنك ترك هذا الحقل فارغاً")]
        [InterestValidation("","")]
        public Guid User { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        [InterestValidation("","")]
        public Guid Class { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        [InterestValidation("FFFBCD3A-F0BB-4BC5-2B2C-08D8B37DCC72", "9E493196-57E8-4019-8ECE-B44ABBD75349")]
        public Guid Subject { get; set; }
    }
}
