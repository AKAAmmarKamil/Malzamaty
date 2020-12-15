using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Malzamaty.Model
{
    public class Class
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Stage { get; set; }
        public string Type { get; set; }
        public Guid Co_ID { get; set; }
        [ForeignKey("Co_ID")]
        public Country Country { get; set; }
    }
}