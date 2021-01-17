using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Malzamaty.Model
{
    public class Interests
    {
        public Guid ID { get; set; }
        public Guid U_ID { get; set; }
        public Guid Su_ID { get; set; }
        public Guid C_ID { get; set; }
        [ForeignKey("U_ID")]
        public User User { get; set; }
        [ForeignKey("Su_ID")]
        public Subject Subject { get; set; }
        [ForeignKey("C_ID")]
        public Class Class { get; set; }


    }
}
