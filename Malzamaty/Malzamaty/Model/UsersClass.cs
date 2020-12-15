using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Model
{
    public class Interests
    {
        [Key]
        public Guid ID { get; set; }
        public Guid U_ID { get; set; }
        [ForeignKey("U_ID")]
        public User User { get; set; }
        public Guid C_ID { get; set; }
        [ForeignKey("C_ID")]
        public Class Class { get; set; }
        public Guid Su_ID { get; set; }
        [ForeignKey("Su_ID")]
        public Subject Subject { get; set; }
    }
}
