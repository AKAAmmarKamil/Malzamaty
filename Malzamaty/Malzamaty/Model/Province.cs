using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Model
{
    public class Province
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CountryID { get; set; }
        [ForeignKey("CountryID")]
        public Country Country { get; set; }
    }
}
