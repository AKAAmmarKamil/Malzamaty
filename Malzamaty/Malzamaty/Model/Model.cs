using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Model
{
    public class Model<TV>
    {
        [Key]
        public TV Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
