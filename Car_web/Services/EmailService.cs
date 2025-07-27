using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public void SendEmail(string subject, string htmlBody)
    {
        var settings = _config.GetSection("EmailSettings");

        var mail = new MailMessage
        {
            From = new MailAddress(settings["SenderEmail"], settings["SenderName"]),
            Subject = subject,
            Body = htmlBody,
            IsBodyHtml = true
        };

        mail.To.Add(settings["SenderEmail"]);

        using var smtp = new SmtpClient(settings["SmtpServer"], int.Parse(settings["SmtpPort"]))
        {
            Credentials = new NetworkCredential(settings["Username"], settings["Password"]),
            EnableSsl = true
        };

        smtp.Send(mail); // 🔁 Sync version
    }
}
