using System.ComponentModel.DataAnnotations;

namespace Malzamaty.Form
{
    public class CodeForm
    {
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        [RegularExpression(@"^[0-9]{6,6}$", ErrorMessage = "error Message ")]
        public string Code { get; set; }

    }
}
