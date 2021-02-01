using System.ComponentModel.DataAnnotations;

namespace Malzamaty.Model.Form
{
    public class LoginForm
    {
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]

        public string Password { get; set; }
    }
}
