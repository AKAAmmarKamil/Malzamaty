using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Dto
{
    public class FileWriteDto
    {
        [Required(ErrorMessage = "لا يمكنك ترك هذا الحقل فارغاً")]
        public string Description { get; set; }

        public string FilePath { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public Guid Class { get; set; }
        public Guid User { get; set; }
        public Guid Subject { get; set; }
    }
}
