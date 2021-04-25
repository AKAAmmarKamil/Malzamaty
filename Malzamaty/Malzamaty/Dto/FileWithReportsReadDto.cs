using System;
using System.Collections.Generic;

namespace Malzamaty.Dto
{
    public class FileWithReportsReadDto
    {
        public Guid Id { get; set; }
        public string FileDescription { get; set; }
        public double Price { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public int PublishDate { get; set; }
        public DateTimeOffset UploadDate { get; set; }
        public int OrderCount { get; set; }
        public string SubjectName { get; set; }
        public string ClassName { get; set; }
        public string ClassType { get; set; }
        public string Stage { get; set; }
        public virtual ICollection<ReportReadDto> Report { get; set; }
    }
}
