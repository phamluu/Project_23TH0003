using Microsoft.AspNetCore.Identity.UI.Services;

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
}
