using System.ComponentModel.DataAnnotations;
namespace Malzamaty.Model
{
    public class Subject
    {
        [Key]
        public string Su_ID { get; set; }
        public string Su_Name { get; set; }
    }
}