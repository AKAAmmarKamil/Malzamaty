using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Malzamaty.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly TheContext _dbContext;
        public FileController(TheContext dbContext)
        {
            _dbContext = dbContext;
        }
        private byte[] ConvertToByte(string filename)
        {
            return System.IO.File.ReadAllBytes(filename);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFiles()
        {
            var result = await _dbContext.File.Select(x => new { x.F_Description, x.F_Author, x.F_Type, x.F_Format, x.F_PublishDate, x.C_ID, x.Student.St_ID, x.Su_ID }).ToListAsync();
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAppropriateFile(string St_ID,DateTime Date, double Rate, bool Reports)
        {
            string Class = await _dbContext.Student.Where(x => x.St_ID == St_ID).Select(x => x.C_ID).FirstAsync();
            string Subject =await _dbContext.Student.Where(x => x.St_ID == St_ID).Select(x => x.Su_ID).FirstAsync();
            if (Reports==true)
            {
                var File = from f in _dbContext.File
                           join c in _dbContext.Class
                           on f.C_ID equals c.C_ID
                           join co in _dbContext.Country
                           on c.Co_ID equals co.Co_ID
                           join St in _dbContext.Student
                           on f.Student.St_ID equals St.St_ID
                           join Ra in _dbContext.Rating
                           on f.F_ID equals Ra.F_ID into Rat
                           from R in Rat.DefaultIfEmpty()
                           join Su in _dbContext.Subject
                           on f.Su_ID equals Su.Su_ID into Sub
                           from o in Sub.DefaultIfEmpty()
                           join Re in _dbContext.Report
                           on f.F_ID equals Re.F_ID into Rep
                           from Repo in Rep.DefaultIfEmpty()
                           where f.C_ID == Class && ((f.Su_ID == Subject) || f.Su_ID == null)&&f.F_PublishDate>=Date && Repo.F_ID == null
                           group f by new { f.F_ID, f.F_Description, f.F_File, f.F_Author, f.F_Type,f.F_Format,f.F_PublishDate, c.C_Name, c.C_Stage, c.C_Type,co.Co_Name, St.St_FullName, o.Su_Name } into g
                           where _dbContext.Rating.Where(x => x.F_ID == g.Key.F_ID).Average(x => x.Ra_Rate) >= Rate
                           select new
                           {
                               g.Key.F_Description,
                               g.Key.F_Author,
                               g.Key.F_Type,
                               g.Key.F_Format,
                               g.Key.F_PublishDate,
                               g.Key.C_Name,
                               g.Key.C_Stage,
                               g.Key.C_Type,
                               g.Key.Co_Name,
                               g.Key.St_FullName,
                               g.Key.Su_Name,
                               Average = _dbContext.Rating.Where(x => x.F_ID == g.Key.F_ID).Average(x => x.Ra_Rate)
                           };
                    return Ok(File);
            }
            else
            {
                           var File = from f in _dbContext.File
                           join c in _dbContext.Class
                           on f.C_ID equals c.C_ID
                           join co in _dbContext.Country
                           on c.Co_ID equals co.Co_ID
                           join St in _dbContext.Student
                           on f.Student.St_ID equals St.St_ID
                           join Ra in _dbContext.Rating
                           on f.F_ID equals Ra.F_ID into Rat
                           from R in Rat.DefaultIfEmpty()
                           join Su in _dbContext.Subject
                           on f.Su_ID equals Su.Su_ID into Sub
                           from o in Sub.DefaultIfEmpty()
                           join Ro in _dbContext.Report
                           on f.F_ID equals Ro.F_ID into Rep
                           from Roo in Rep.DefaultIfEmpty()

                           where f.C_ID == Class && ((f.Su_ID == Subject) || f.Su_ID == null)&&f.F_PublishDate>=Date
                           group f by new { f.F_ID, f.F_Description, f.F_File, f.F_Author, f.F_Type,f.F_Format,f.F_PublishDate, c.C_Name, c.C_Stage, c.C_Type,co.Co_Name, St.St_FullName, o.Su_Name } into g
                           where _dbContext.Rating.Where(x => x.F_ID == g.Key.F_ID).Average(x => x.Ra_Rate) >= Rate
                           select new
                           {
                               g.Key.F_Description,
                               g.Key.F_Author,
                               g.Key.F_Type,
                               g.Key.F_Format,
                               g.Key.F_PublishDate,
                               g.Key.C_Name,
                               g.Key.C_Stage,
                               g.Key.C_Type,
                               g.Key.Co_Name,
                               g.Key.St_FullName,
                               g.Key.Su_Name,
                               Average = _dbContext.Rating.Where(x => x.F_ID == g.Key.F_ID).Average(x => x.Ra_Rate),
                               Report = from Re in _dbContext.Report
                                        where Re.F_ID == g.Key.F_ID
                                        select new { Description = Re.R_Description }
                           };
                           return Ok(File);
            }
        }
        [HttpGet]
        public async Task<FileStreamResult> GetFile(string F_ID)
        {
            byte[] result = await _dbContext.File.Where(x => x.F_ID == F_ID).Select(x => x.F_File).FirstAsync();
            string Type = await _dbContext.File.Where(x => x.F_ID == F_ID).Select(x => x.F_Format).FirstAsync();
            MemoryStream ms = new MemoryStream(result);
            return new FileStreamResult(ms, "application/" + Type.ToString());
        }
        [HttpPost]
        public async Task<IActionResult> AddFile([FromBody]Model.File file,string St_ID,string @Path)
        {
            int Different = 0;
            string Format = "";
            var Exist =await _dbContext.Exist.Where(x => x.C_ID == file.C_ID && x.Su_ID == file.Su_ID).Select(x => x.E_ID).FirstOrDefaultAsync();
            for (int i = 0; i < Path.Length; i++)
                if (Path[i] == '.')
                {
                    Different = Path.Length - (i+1);
                    for (int j = i + 1; j < Path.Length; j++)
                    Format += Path[j];
                }
            if (Exist != null)
            {

                var con = new SqlConnection("Server=DESKTOP-A1QK3DR;Database=Malzamaty;Trusted_Connection=True;ConnectRetryCount=0");
                var cmd = new SqlCommand("AddFile", con);
                await con.OpenAsync();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@F_Description", file.F_Description);
                cmd.Parameters.AddWithValue("@F_File", ConvertToByte(@Path));
                cmd.Parameters.AddWithValue("@F_Author", file.F_Author);
                cmd.Parameters.AddWithValue("@F_Type", file.F_Type);
                cmd.Parameters.AddWithValue("@F_Format", Format);
                cmd.Parameters.AddWithValue("@C_ID", file.C_ID);
                cmd.Parameters.AddWithValue("@St_ID", St_ID);
                cmd.Parameters.AddWithValue("@Su_ID", file.Su_ID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return Ok(dt);
            }
            else return BadRequest("هذه المادة ليست متاحة");
            }
        [HttpPut]
        public async Task<IActionResult> UpdateFile([FromBody]Report report, string R_ID)
        {
            var Rep = _dbContext.Report.Find(R_ID);
            Rep.R_Description = report.R_Description;
            _dbContext.Entry(Rep).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFile(string F_ID)
        {
            var file = new Model.File { F_ID = F_ID };
            _dbContext.Entry(file).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}