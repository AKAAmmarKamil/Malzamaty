using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Malzamaty.Model
{
    public class Interests
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid SubjectID { get; set; }
        public Guid ClassID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        [ForeignKey("SubjectID")]
        public Subject Subject { get; set; }
        [ForeignKey("ClassID")]
        public Class Class { get; set; }


    }
}
