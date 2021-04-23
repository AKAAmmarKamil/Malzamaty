using System;

namespace Malzamaty.Dto
{
    public class LibraryReadDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public AddressReadDto Address { get; set; }
    }
}
