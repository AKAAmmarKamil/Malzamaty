﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Model
{
    public class District
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProvinceID { get; set; }
        [ForeignKey("ProvinceID")]
        public Province Province { get; set; }

    }
}
