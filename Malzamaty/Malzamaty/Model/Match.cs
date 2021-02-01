using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Malzamaty.Model
{
    public class Match
    {
        public Guid ID { get; set; }
        public Guid ClassID { get; set; }
        [ForeignKey("ClassID")]
        public Class Class { get; set; }
        public Guid SubjectID { get; set; }
        [ForeignKey("SubjectID")]
        public Subject Subject { get; set; }
    }
}