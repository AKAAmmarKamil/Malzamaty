using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Malzamaty.Model
{
    public class Exist
    {
        [Key]
        public string E_ID { get; set; }
        public string C_ID { get; set; }
        [ForeignKey("C_ID")]
        public Class Class { get; set; }
        public string Su_ID { get; set; }
        [ForeignKey("Su_ID")]
        public Subject Subject { get; set; }
    }
}