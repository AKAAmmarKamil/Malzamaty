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
        public int DownloadCount { get; set; }
        [ForeignKey("C_ID")]
        public Class Class { get; set; }
        [ForeignKey("User_ID")]
        public User User { get; set; }
        [ForeignKey("Subject_ID")]
        public Subject Subject { get; set; }
    }
}