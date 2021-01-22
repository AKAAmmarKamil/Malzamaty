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
            var mail = new MailMessage("AmmarKamil909@gmail.com",Email,"تغيير كلمة السر",
                "لقد طلبت تغيير كلمة المرور الخاصة بحسابك على منصة ملزمتي يرجى إدخال هذا الرمز أولاً\n"+Code);
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("AmmarKamil909@gmail.com", "Gou=aka333@"),
                EnableSsl = true,
            };

            smtpClient.Send(mail);
            return Code;
        }
    }
}
