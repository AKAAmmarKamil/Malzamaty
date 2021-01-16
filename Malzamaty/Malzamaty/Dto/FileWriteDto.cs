using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Dto
{
    public class FileWriteDto
    {
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public DateTimeOffset PublishDate { get; set; }
        public int DownloadCount { get; set; }
        public Guid Class { get; set; }
        public Guid User { get; set; }
        public Guid Subject { get; set; }
    }
}
