using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Malzamaty.Model
{
    public class Rating
    {
        [Key]
        public Guid ID { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
        [ForeignKey("Us_ID")]
        public User User { get; set; }
        public Guid F_ID { get; set; }
        public File File { get; set; }
    }
}