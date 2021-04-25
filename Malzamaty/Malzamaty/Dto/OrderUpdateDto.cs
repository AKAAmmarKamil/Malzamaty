using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Dto
{
    public class OrderUpdateDto
    {
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        [Range(0,2,ErrorMessage = "حالة الطلب غير صحيحة")]

        public int OrderStatus { get; set; }
    }
}
