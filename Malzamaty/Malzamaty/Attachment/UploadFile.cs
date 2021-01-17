using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Malzamaty.Attachment {
    public class UploadFile {
        private readonly IHostingEnvironment _environment;

        public UploadFile(IHostingEnvironment environment) { _environment = environment; }

        public async Task<string> Upload(string bas64)
        {
            var Format =bas64.Split(".");
            var strm = bas64;
            var filName = Guid.NewGuid();
            var filepath = _environment.ContentRootPath + @"/wwwroot/Files/" + filName + "."+Format[1];
            var bytess = Convert.FromBase64String(strm);

            using (var fileStream = new FileStream(filepath, FileMode.Create)) {
                 await fileStream.WriteAsync(bytess, 0, bytess.Length);
                 await fileStream.FlushAsync();
            }

            return filName.ToString();
        }

        public static bool IsBase64(string base64String) {
            var ok = true;
            if (string.IsNullOrEmpty(base64String)) {
                ok = false;
            }

            if (base64String.Length % 4 != 0) {
                ok = false;
            }

            if (base64String.Contains(" ") && !base64String.Contains("\t")) {
                ok = false;
            }

            if (base64String.Contains("\r")) {
                ok = false;
            }

            if (base64String.Contains("\n")) {
                ok = false;
            }

            return ok;
        }
    }
}