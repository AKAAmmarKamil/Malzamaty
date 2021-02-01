using System;
using System.Collections.Generic;

namespace Malzamaty.Dto
{
    public class UserReadDto
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public List<InterestReadDto> Interests { get; set; }

    }
}
