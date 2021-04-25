using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Form
{
    public class OrderFile
    {
        public Guid FileId { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
    }
}
