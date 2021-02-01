using System;
using System.ComponentModel.DataAnnotations;
namespace Malzamaty.Model
{
    public class User
    {
        [Key]
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Activated { get; set; }
    }
}