using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Form
{
    public class EmailForm
    {
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        [EmailAddress(ErrorMessage = "البريد الألكتروني غير صحيح")]
        public string Email { get; set; }
    }
}
