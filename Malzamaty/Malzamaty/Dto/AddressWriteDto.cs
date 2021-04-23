using System;
using System.ComponentModel.DataAnnotations;
namespace Malzamaty.Dto
{
    public class AddressWriteDto
    {
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public Guid CountryID { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public Guid ProvinceID { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public Guid DistrictID { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public Guid MahallahID { get; set; }
        public string? Details { get; set; }
    }
}
