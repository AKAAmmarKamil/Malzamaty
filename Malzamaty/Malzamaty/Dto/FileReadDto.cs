using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Dto
{
    public class FileReadDto
    {
        public Guid Id { get; set; }
        public string FileDescription { get; set; }
        public string FilePath { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public int PublishDate { get; set; }
        public int DownloadCount { get; set; }
        public string SubjectName { get; set; }
        public string ClassName { get; set; }
        public string UserName { get; set; }

    }
}
