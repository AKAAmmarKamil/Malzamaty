using System.ComponentModel.DataAnnotations;
namespace Malzamaty.Dto
{
    public class SubjectWriteDto
    {
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public string Name { get; set; }
    }
}
