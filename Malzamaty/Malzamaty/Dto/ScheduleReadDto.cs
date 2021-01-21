using System;
namespace Malzamaty.Dto
{
    public class ScheduleReadDto
    {
        public Guid Id { get; set; }
        public DateTime? StartStudy { get; set; }
        public DateTime? FinishStudy { get; set; }
        public string SubjectName { get; set; }
    }
}
