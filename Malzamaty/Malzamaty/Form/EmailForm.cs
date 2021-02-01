﻿using System.ComponentModel.DataAnnotations;

namespace Malzamaty.Form
{
    public class EmailForm
    {
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        [EmailAddress(ErrorMessage = "البريد الألكتروني غير صحيح")]
        public string Email { get; set; }
    }
}
