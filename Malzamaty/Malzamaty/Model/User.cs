using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Malzamaty.Model
{
    public class User
    {
        [Key]
        public Guid ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid Authentication { get; set; }
        [ForeignKey("Authentication")]
        public Roles Roles { get; set; }
        //public string C_ID { get; set; }

        //[ForeignKey("C_ID")]
        //public Class Class { get; set; }
        // public string Su_ID { get; set; }
        //[ForeignKey("Su_ID")]
        // public Subject Subject { get; set; }
    }
}