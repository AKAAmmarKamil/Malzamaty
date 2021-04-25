using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Dto
{
    public class TaxesWriteDto
    {
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public double DeliveryTaxes { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        [Range(0, 100, ErrorMessage = "الخصم غير صحيح")]
        public double DeliveryDiscount { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public double MalzamatyTaxes { get; set; }
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        [Range(0,100,ErrorMessage = "الخصم غير صحيح")]
        public double MalzamatyDiscount { get; set; }
    }
}
