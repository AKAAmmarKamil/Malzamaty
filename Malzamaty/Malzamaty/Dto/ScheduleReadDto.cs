using System;
namespace Malzamaty.Dto
{
    public class ScheduleReadDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset StartStudy { get; set; }
        public DateTimeOffset FinishStudy { get; set; }
        public string SubjectName { get; set; }
    }
}
