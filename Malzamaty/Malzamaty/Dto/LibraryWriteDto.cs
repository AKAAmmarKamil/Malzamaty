using System;
using System.ComponentModel.DataAnnotations;
namespace Malzamaty.Dto
{
    public class LibraryWriteDto
    {
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public AddressWriteDto Address { get; set; }
    }
}
