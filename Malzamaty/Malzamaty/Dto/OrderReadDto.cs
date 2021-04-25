using System;

namespace Malzamaty.Dto
{
    public class OrderReadDto
    {
        public Guid Id { get; set; }
        public AddressReadDto From { get; set; }
        public AddressReadDto To { get; set; }
        public FileReadDto File { get; set; }
        public bool IsBestCustomer { get; set; }
    }
}
