using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Malzamaty.Model
{
    public class Class
    {
        [Key]
        public Guid ID { get; set; }
        public string ClassName { get; set; }
        public Guid S_ID { get; set; }
        [ForeignKey("S_ID")]
        public Stage Stage { get; set; }
        public Guid T_ID { get; set; }
        [ForeignKey("T_ID")]
        public ClassType ClassType { get; set; }
        public Guid Co_ID { get; set; }
        [ForeignKey("Co_ID")]
        public Country Country { get; set; }
    }
}