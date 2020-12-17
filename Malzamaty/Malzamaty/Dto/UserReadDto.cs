using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Dto
{
    public class UserReadDto
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public List<Class> Classes { get; set; }
        public List<Subject> Subjects { get; set; }

    }
}
