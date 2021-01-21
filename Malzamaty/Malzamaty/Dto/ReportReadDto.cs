using System;
namespace Malzamaty.Dto
{
    public class ReportReadDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
