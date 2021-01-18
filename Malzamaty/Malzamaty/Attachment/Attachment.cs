using System;
using System.IO;
using System.Threading.Tasks;
using Malzamaty.Form;
using Microsoft.AspNetCore.Hosting;

namespace Malzamaty.Attachment {
    public class Attachment {
        public static async Task<byte[]> ConvertToBytes(string Path) => File.ReadAllBytes(Path);
        public static async Task<AttachmentString> Upload(byte[] bytes, string filepath, string Type)
        {
            var GuidKey = Guid.NewGuid();
            var filName = filepath + GuidKey + "." + Type;
            using (var fileStream = new FileStream(filName, FileMode.Create))
            {
                await fileStream.WriteAsync(bytes, 0, bytes.Length);
                await fileStream.FlushAsync();
            }
            var AttachmentString = new AttachmentString();
            AttachmentString.Body = GuidKey + "." + Type.ToString();
            return AttachmentString;
        }
    }
}