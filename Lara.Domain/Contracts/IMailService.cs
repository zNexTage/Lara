using System.Net.Mail;

namespace Lara.Domain.Contracts;

public interface IMailService
{
    public void SendMessage(string to, string subject, MailMessage message);
}