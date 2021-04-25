using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Model
{
    
    public class Taxes
    {
        public Guid Id { get; set; }
        public double DeliveryTaxes { get; set; }
        public double DeliveryDiscount { get; set; }
        public double MalzamatyTaxes { get; set; }
        public double MalzamatyDiscount { get; set; }
    }
}
