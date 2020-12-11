using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Malzamaty.Model
{
    public class Class
    {
        [Key]
        public string C_ID { get; set; }
        public string C_Name { get; set; }
        public string C_Stage { get; set; }
        public string C_Type { get; set; }
        public string Co_ID { get; set; }
        [ForeignKey("Co_ID")]
        public Country Country { get; set; }
    }
}