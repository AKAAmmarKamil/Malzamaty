using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Malzamaty.Model
{
    public class Address
    {
        public Guid Id { get; set; }
        public Guid CountryID { get; set; }
        [ForeignKey("CountryID")]
        public Country Country { get; set; }
        public Guid ProvinceID { get; set; }
        [ForeignKey("ProvinceID")]
        public Province Province { get; set; }
        public Guid DistrictID { get; set; }
        [ForeignKey("DistrictID")]
        public District District { get; set; }
        public Guid MahallahID { get; set; }
        [ForeignKey("MahallahID")]
        public Mahallah Mahallah { get; set; }
        public string? Details { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

        public virtual User User { get; set; }
        public virtual Library Library { get; set; }
    }
}
