using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Malzamaty.Model
{
    public class File
    {
        [Key]
        public string F_ID { get; set; }
        public string F_Description { get; set; }
        public byte[] F_File { get; set; }
        public string F_Author { get; set; }
        public string F_Type { get; set; }
        public string F_Format {get;set;}
        public DateTimeOffset F_PublishDate { get; set; }
        public string C_ID { get; set; }
        public Class Class { get; set; }
        [ForeignKey("St_ID")]
        public Student Student { get; set; }
        public string Su_ID { get; set; }
        public Subject Subject { get; set; }
    }
}