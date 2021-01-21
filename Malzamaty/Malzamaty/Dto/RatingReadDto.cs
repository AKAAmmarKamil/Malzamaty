using System;
namespace Malzamaty.Dto
{
    public class RatingReadDto
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
    }
}
