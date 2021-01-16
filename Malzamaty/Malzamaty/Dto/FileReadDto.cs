using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Dto
{
    public class FileReadDto
    {
        public string FileDescription { get; set; }
        public string FilePath { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public DateTimeOffset PublishDate { get; set; }
        public int DownloadCount { get; set; }
        public string Class { get; set; }
        public string Stage { get; set; }
        public string ClassType { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string SubjectName { get; set; }
    }
}
