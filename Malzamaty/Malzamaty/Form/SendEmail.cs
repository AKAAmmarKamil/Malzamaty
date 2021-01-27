using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
namespace Malzamaty.Form
{
    public class SendEmail
    {
       
        public static string SendMessage(string Email)
        {
            var Code = new Random().Next(0, 1000000).ToString("D6");
            var Domain = "@"+Email.Split("@")[1];
            var StarsToString = "*****";           
            var HideEmail = Email.Substring(0,2)+StarsToString + Domain;
            var Message = GetMessage(Code, HideEmail);
            var mail = new MailMessage("AmmarKamil909@gmail.com",Email,"تغيير كلمة مرور",Message);
            mail.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("AmmarKamil909@gmail.com", "Gou=aka333@"),
                EnableSsl = true,
            };
            smtpClient.Send(mail);
            return Code;
        }
        public static string GetMessage(string Code,string Email)
        {
            return @"<html><Body><table dir ='rtl'><tbody><tr><td> حساب ملزمتي </td></tr><tr><td> رمز إعادة تعيين كلمة مرور</td></tr><tr><td> الرجاء استخدام هذا الرمز لإعادة تعيين كلمة مرور حساب &lrm;<a href='mailto:am*****@gmail.com' target='_blank'>am***** @gmail.com</a>&nbsp; على ملزمتي.</td></tr><tr><td> فيما يلي الرمز الخاص بك:<span id = 'CodeId' > 7151647 </ span >
            < button onClick= 'copy()' > نسخ الرمز</button></td></tr><tr><td><tr><td> شكرًا،</td></tr><tr><td> فريق حساب ملزمتي</td></tr></tbody></table></Body><script>function copy() {var copyText = document.getElementById('CodeId');
            var textArea = document.createElement('textarea');
            textArea.value = copyText.textContent;
            document.body.appendChild(textArea);
            textArea.select();
            document.execCommand('Copy');
            textArea.remove();
            }
            </script>
            </html>";
        }
    }
}
