using System;

namespace Malzamaty.Dto
{
    public class OrderReadDto
    {
        public Guid Id { get; set; }
        public AddressReadDto UserAddress { get; set; }
        public AddressReadDto LibraryAddress { get; set; }
        public bool IsDelivered { get; set; }
    }
}
