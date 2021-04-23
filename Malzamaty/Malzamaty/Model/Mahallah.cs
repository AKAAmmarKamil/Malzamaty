using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Model
{
    public class Mahallah
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid DistrictID { get; set; }
        [ForeignKey("DistrictID")]
        public District District { get; set; }
    }
}
