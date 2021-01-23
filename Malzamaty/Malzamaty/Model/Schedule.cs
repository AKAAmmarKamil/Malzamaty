using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Malzamaty.Model
{
    public class Schedule
    {
        [Key]
        public Guid ID { get; set; }
        public DateTimeOffset StartStudy { get; set; }
        public DateTimeOffset FinishStudy { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        [ForeignKey("SubjectID")]
        public Subject Subject { get; set; }
    }
}