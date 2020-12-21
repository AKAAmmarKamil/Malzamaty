using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Dto
{
    public class ClassReadDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Stage { get; set; }
        public string ClassType { get; set; }
        public string Country { get; set; }
    }
}
