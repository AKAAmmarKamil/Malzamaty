using System;
namespace Malzamaty.Dto
{
    public class ReportReadDto
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        public FileReadDto File { get; set; }

    }
}
