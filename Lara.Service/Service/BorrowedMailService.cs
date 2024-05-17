
using System.Net;
using System.Net.Mail;
using Lara.Data.Migrations;
using Lara.Domain.DataTransferObjects;
using Lara.Domain.Entities;
using Lara.Service.Configuration;

namespace Lara.Service.Service;

public class BorrowedMailService : BaseMailService
{

    public BorrowedMailService(EmailConfiguration emailConfiguration) : base(emailConfiguration)
    {
    }

    public string GetMessageHTMLTemplate()
    {
        var path = Path.Combine("StaticFiles", "Html", "EmailTemplates", "BorrowedMailTemplate.html");
        var body = string.Empty;
        
        using (StreamReader stream = new(path))
        {
            body = stream.ReadToEnd();
        }

        return body;
    }

    public MailMessage CreateMessage(string to, string subject, BorrowedBook borrowedBook)
    {
        var message = GetMessageHTMLTemplate();
        message = message.Replace("{nome_usuario}", borrowedBook.User.FirstName);
        message = message.Replace("{nome_livro}", borrowedBook.Book.Title);
        message = message.Replace("{prazo_devolucao}", $"{BorrowedBook.RETURN_DATE_DAYS} dias");
        message = message.Replace("{data_devolucao}", borrowedBook.ReturnDate.ToString("d"));
        message = message.Replace("{imagem_livro}", borrowedBook.Book.Image);
        
        var emailMessage = new MailMessage();

        emailMessage.From = new MailAddress(_emailConfiguration.Email);
        emailMessage.To.Add(to);
        emailMessage.Subject = subject;
        emailMessage.IsBodyHtml = true;
        emailMessage.Body = message;

        return emailMessage;
    }
}