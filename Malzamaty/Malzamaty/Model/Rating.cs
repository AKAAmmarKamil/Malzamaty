using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Malzamaty.Model
{
    public class Rating
    {
        [Key]
        public string Ra_ID { get; set; }
        public string Ra_Comment { get; set; }
        public int Ra_Rate { get; set; }
        [ForeignKey("St_ID")]
        public Student Student { get; set; }
        public string F_ID { get; set; }
        public File File { get; set; }
    }
}