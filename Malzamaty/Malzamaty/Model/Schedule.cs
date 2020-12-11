using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Malzamaty.Model
{
    public class Schedule
    {
        [Key]
        public string Sc_ID { get; set; }
        public DateTime? StartStudy { get; set; }
        public DateTime? FinishStudy { get; set; }
        [ForeignKey("St_ID")]
        public Student Student { get; set; }
        [ForeignKey("Su_ID")]
        public Subject Subject { get; set; }
    }
}