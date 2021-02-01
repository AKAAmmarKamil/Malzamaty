using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Malzamaty.Model
{
    public class Class
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid StageID { get; set; }
        [ForeignKey("StageID")]
        public Stage Stage { get; set; }
        public Guid ClassTypeID { get; set; }
        [ForeignKey("ClassTypeID")]
        public ClassType ClassType { get; set; }
        public Guid CountryID { get; set; }
        [ForeignKey("CountryID")]
        public Country Country { get; set; }
    }
}