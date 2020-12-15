using System;
using System.ComponentModel.DataAnnotations;
namespace Malzamaty.Model
{
    public class Country
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
    }
}