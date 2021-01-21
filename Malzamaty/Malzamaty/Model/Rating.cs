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
        [ForeignKey("UserID")]
        public User User { get; set; }
        [ForeignKey("FileID")]
        public File File { get; set; }
    }
}