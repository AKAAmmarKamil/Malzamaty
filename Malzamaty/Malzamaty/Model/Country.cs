using System.ComponentModel.DataAnnotations;
namespace Malzamaty.Model
{
    public class Country
    {
        [Key]
        public string Co_ID { get; set; }
        public string Co_Name { get; set; }
    }
}