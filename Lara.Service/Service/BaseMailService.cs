using System.Net;
using System.Net.Mail;
using Lara.Domain.Contracts;
using Lara.Service.Configuration;

namespace Lara.Service.Service;

public class BaseMailService : IMailService
{
    protected readonly EmailConfiguration _emailConfiguration;

    public BaseMailService(EmailConfiguration emailConfiguration)
    {
        _emailConfiguration = emailConfiguration;
    }
    
    public void SendMessage(string to, string subject, MailMessage message)
    {
        using var client = new SmtpClient(
            _emailConfiguration.Host, 
            _emailConfiguration.Port);
        
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential(_emailConfiguration.Email, _emailConfiguration.Password);
            
        client.Send(message);
    }
}