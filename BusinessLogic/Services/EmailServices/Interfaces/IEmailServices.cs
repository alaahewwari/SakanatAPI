using BusinessLogic.Infrastructure;

namespace BusinessLogic.Services.EmailServices.Interfaces;

public interface IEmailServices
{
    //old
    //public void SendEmailAsync(Message message);
    Task SendEmailAsync(string to, string subject, string body);
    Task SendConfirmationEmailAsync(string to, string url);
    Task SendPasswordResetEmailAsync(string to, string url);
}