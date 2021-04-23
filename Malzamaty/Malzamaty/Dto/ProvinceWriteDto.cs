using System;
using System.ComponentModel.DataAnnotations;
namespace Malzamaty.Dto
{
    public class ProvinceWriteDto
    {
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public string Name { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public Guid Country { get; set; }
    }
}
