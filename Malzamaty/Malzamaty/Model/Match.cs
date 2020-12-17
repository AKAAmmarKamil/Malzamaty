using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Malzamaty.Model
{
    public class Match
    {
        [Key]
        public Guid ID { get; set; }
        public Guid C_ID { get; set; }
        [ForeignKey("C_ID")]
        public Class Class { get; set; }
        public Guid Su_ID { get; set; }
        [ForeignKey("Su_ID")]
        public Subject Subject { get; set; }
    }
}