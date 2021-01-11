﻿using System;
using System.ComponentModel.DataAnnotations;
namespace Malzamaty.Dto
{
    public class InterestWriteDto
    {
        [Required(ErrorMessage ="لا يمكنك ترك هذا الحقل فارغاً")]
        public Guid User { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public Guid Class { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public Guid Subject { get; set; }
    }
}