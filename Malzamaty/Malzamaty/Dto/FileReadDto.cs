﻿using System;
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
        public DateTimeOffset PublishDate { get; set; }
        public int DownloadCount { get; set; }
        public ClassReadDto Class { get; set; }
        public UserReadDto User { get; set; }
        public SubjectReadDto Subject { get; set; }

    }
}
