﻿using System;
using System.ComponentModel.DataAnnotations;
namespace Malzamaty.Dto
{
    public class DistrictWriteDto
    {
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public string Name { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public Guid Province { get; set; }

    }
}
