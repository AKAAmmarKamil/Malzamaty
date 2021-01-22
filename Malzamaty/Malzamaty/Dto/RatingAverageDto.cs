using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Dto
{
    public class RatingAverageDto
    {
        [Display(Name ="Rating Average")]
        public double RatingAverage { get; set; }
    }
}
