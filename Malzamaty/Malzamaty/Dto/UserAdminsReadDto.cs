using System;
using System.Collections.Generic;

namespace Malzamaty.Dto
{
    public class UserAdminsReadDto
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
