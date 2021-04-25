using System;

namespace Malzamaty.Dto
{
    public class AddressReadDto
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Mahallah { get; set; }
        public string Details { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
