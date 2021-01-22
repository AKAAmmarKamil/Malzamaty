﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Form
{
    public class CodeForm
    {
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        // [RegularExpression(@"^(\d{6})$", ErrorMessage = "الرمز مكون من 6 أرقام")]
        [RegularExpression(@"^[0-9]{6,6}$", ErrorMessage = "error Message ")]
        public string Code { get; set; }

    }
}