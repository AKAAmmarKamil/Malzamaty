using System;

namespace Malzamaty.Dto
{
    public class OrderFileReadDto
    {
        public Guid Id { get; set; }
        public AddressReadDto To { get; set; }
        public FileReadDto File { get; set; }
    }
}
