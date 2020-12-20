using System.ComponentModel.DataAnnotations;
namespace Malzamaty.Dto
{
    public class RolesWriteDto
    {
        [Required(ErrorMessage ="لا يمكنك ترك هذا الحقل فارغاً")]
        public string Role { get; set; }
    }
}
