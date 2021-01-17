using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Model.Form
{
    public class ChangePasswordForm
    {
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        [MinLength(8, ErrorMessage = "كلمة السر يجب ان تكون 8 أحرف كحد أدنى")]
        public string Password { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        [Compare(nameof(Password), ErrorMessage = "كلمتا السر غير متطابقتان")]
        public string ReWritePassword { get; set; }
    }
}
