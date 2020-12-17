using System.ComponentModel.DataAnnotations;
namespace Malzamaty.Dto
{
    public class CountryWriteDto
    {
        [Required(ErrorMessage ="لا يمكنك ترك هذا الحقل فارغاً")]
        public string Name { get; set; }
    }
}
