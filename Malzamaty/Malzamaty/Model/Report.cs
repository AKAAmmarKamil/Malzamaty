using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Malzamaty.Model
{
    public class Report
    {
        [Key]
        public Guid ID { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        [ForeignKey("FileID")]
         public File File { get; set; }
    }
}