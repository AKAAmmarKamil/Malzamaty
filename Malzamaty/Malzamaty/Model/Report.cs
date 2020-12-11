using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Malzamaty.Model
{
    public class Report
    {
        [Key]
        public string R_ID { get; set; }
        public string R_Description { get; set; }
        public DateTimeOffset R_Date { get; set; }
        public string F_ID { get; set; }
        [ForeignKey("F_ID")]
        public File File { get; set; }
    }
}