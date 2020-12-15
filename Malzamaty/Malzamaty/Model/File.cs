using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Malzamaty.Model
{
    public class File
    {
        [Key]
        public Guid ID { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public string Format {get;set;}
        public DateTimeOffset PublishDate { get; set; }
        public Guid C_ID { get; set; }
        public Class Class { get; set; }
        [ForeignKey("Us_ID")]
        public User User { get; set; }
        public Guid Su_ID { get; set; }
        public Subject Subject { get; set; }
    }
}