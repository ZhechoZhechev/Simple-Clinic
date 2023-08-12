namespace SimpleClinic.Common.Helpers;

using Microsoft.Extensions.Configuration;

using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

public class EmailService
{
    private readonly IConfiguration configuration;

    public EmailService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void SendMailWhenCancelBooking(string email, string name, string phone, string message)
    {
            var smtpConfig = configuration.GetSection("Smtp");
            var smtpHost = smtpConfig["Host"];
            var smtpPort = int.Parse(smtpConfig["Port"]);
            var smtpUsername = smtpConfig["Username"];
            var smtpPassword = smtpConfig["Password"];

            var messageBody = $"Email: {email}\nName: {name}\nPhone: {phone}\nMessage: {message}";

            var messageToSend = new MimeMessage();
            messageToSend.From.Add(new MailboxAddress("", smtpUsername));
            messageToSend.To.Add(new MailboxAddress("", email));
            messageToSend.Subject = $"Appointment with patient {name} has been canceled";
            messageToSend.Body = new TextPart("plain")
            {
                Text = messageBody
            };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(smtpHost, smtpPort, SecureSocketOptions.Auto);
                smtpClient.Authenticate(smtpUsername, smtpPassword);
                smtpClient.Send(messageToSend);
                smtpClient.Disconnect(true);
            }
    }
}
