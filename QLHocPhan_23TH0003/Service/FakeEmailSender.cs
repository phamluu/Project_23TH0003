using Microsoft.AspNetCore.Identity.UI.Services;
using QLHocPhan_23TH0003.Common.Helpers;
using System.Net;
using System.Net.Mail;

namespace QLHocPhan_23TH0003.Service
{
    public class FakeEmailSender: IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine($"Fake email to: {email}, subject: {subject}");
            return Task.CompletedTask;
        }
    }

    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public SmtpEmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string? userMail = CauHinhHelper.Get("smtp_user");
            string? userPass = CauHinhHelper.Get("smtp_pass");

            if (string.IsNullOrWhiteSpace(userMail) || string.IsNullOrWhiteSpace(userPass))
            {
                // Không throw nữa
                Console.WriteLine("Lỗi: smtp_user hoặc smtp_pass chưa được cấu hình.");
                return;
            }

            try
            {
                using var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(userMail, userPass),
                    EnableSsl = true,
                };

                var mail = new MailMessage
                {
                    From = new MailAddress(userMail),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true
                };
                mail.To.Add(email);

                await smtpClient.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                // Log lỗi, nhưng không ném exception nữa
                Console.WriteLine($"Gửi email thất bại: {ex.Message}");
            }
        }

        //public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        //{
        //    string UserMail = CauHinhHelper.Get("smtp_user");
        //    string UserPass = CauHinhHelper.Get("smtp_pass");
        //    var smtpClient = new SmtpClient("smtp.gmail.com")  // thay thế bằng SMTP bạn dùng
        //    {
        //        Port = 587,
        //        //Credentials = new NetworkCredential(_config["Email:User"], _config["Email:Password"]),
        //        Credentials = new NetworkCredential(UserMail, UserPass),
        //        EnableSsl = true,
        //    };

        //    var mail = new MailMessage
        //    {
        //        From = new MailAddress(UserMail),
        //        Subject = subject,
        //        Body = htmlMessage,
        //        IsBodyHtml = true
        //    };
        //    mail.To.Add(email);

        //    await smtpClient.SendMailAsync(mail);
        //}
    }

}
