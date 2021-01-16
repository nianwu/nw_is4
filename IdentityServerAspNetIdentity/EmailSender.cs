using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace IdentityServerAspNetIdentity
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // 设置邮件内容
            var mail = new MailMessage(
                new MailAddress("1262163090@qq.com", "袁智远"),
                new MailAddress(email)
                );
            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = Encoding.UTF8;
            mail.Priority = MailPriority.High;//邮件优先级

            // 设置SMTP服务器
            var smtp = new SmtpClient("smtp.qq.com", 465);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("1262163090@qq.com", "oyejhzrqztlwgfif");
            // smtp.Credentials = new System.Net.NetworkCredential("yuanzhiyuan@aimiaobi.com", "Yuan1262163090");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtp.EnableSsl = true;
            smtp.Timeout = (int)TimeSpan.FromSeconds(5).TotalMilliseconds;

            await smtp.SendMailAsync(mail);
        }
    }
}