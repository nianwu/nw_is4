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
                new MailAddress("wangbin5542@163.com", "王彬"),
                new MailAddress(email)
                );
            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = Encoding.UTF8;
            mail.Priority = MailPriority.High;//邮件优先级

            // 设置SMTP服务器
            var smtp = new SmtpClient("smtp.163.com", 25);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("yuanzhiyuan@aimiaobi.com", "Yuan1262163090");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            await smtp.SendMailAsync(mail);
        }
    }
}