using System;
using System.ComponentModel.DataAnnotations;

namespace Malzamaty.Utils {
    public class Model<TV> {
        [Key]
        public TV Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}