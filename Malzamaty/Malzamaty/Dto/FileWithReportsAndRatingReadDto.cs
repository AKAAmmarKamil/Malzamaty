using System;
using System.Collections.Generic;

namespace Malzamaty.Dto
{
    public class FileWithReportsAndRatingReadDto
    {
        public Guid Id { get; set; }
        public string FileDescription { get; set; }
        public string FilePath { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public int PublishDate { get; set; }
        public DateTimeOffset UploadDate { get; set; }
        public int DownloadCount { get; set; }
        public string SubjectName { get; set; }
        public string ClassName { get; set; }
        public string ClassType { get; set; }
        public string Stage { get; set; }
        public string UserName { get; set; }
        public double Rate { get; set; }
        public virtual ICollection<ReportReadDto> Report { get; set; }
        public ICollection<RatingReadDto> Rating { get; set; }

    }
}
