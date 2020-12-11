using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Malzamaty.Model
{
    public class Student
    {
        [Key]
        public string St_ID { get; set; }
        public string St_FullName { get; set; }
        public string St_Email { get; set; }
        public string St_Password { get; set; }
        public string St_Authentication { get; set; }
        public string C_ID { get; set; }
        [ForeignKey("C_ID")]
        public Class Class { get; set; }
        public string Su_ID { get; set; }
       [ForeignKey("Su_ID")]
        public Subject Subject { get; set; }
    }
}