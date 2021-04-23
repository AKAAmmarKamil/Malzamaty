using System;
using System.Collections.Generic;
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
        public string Type { get; set; }
        public int PublishDate { get; set; }
        public DateTimeOffset UploadDate { get; set; }
        public int OrderCount { get; set; }
        [ForeignKey("ClassID")]
        public Class Class { get; set; }
        [ForeignKey("AuthorID")]
        public User Author { get; set; }
        [ForeignKey("SubjectID")]
        public Subject Subject { get; set; }
        public Guid? LibraryID { get; set; }
        [ForeignKey("LibraryID")]
        public Library Library { get; set; }
        public virtual ICollection<Report> Report { get; set; }
        public virtual IEnumerable<Rating> Rating { get; set; }
    }
}