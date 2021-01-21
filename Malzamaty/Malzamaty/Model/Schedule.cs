using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Malzamaty.Model
{
    public class Schedule
    {
        [Key]
        public Guid ID { get; set; }
        public DateTime? StartStudy { get; set; }
        public DateTime? FinishStudy { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        [ForeignKey("SubjectID")]
        public Subject Subject { get; set; }
    }
}