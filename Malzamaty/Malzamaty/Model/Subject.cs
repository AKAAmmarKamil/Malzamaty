using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Malzamaty.Model
{
    public class Subject
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }

    }
}