﻿namespace SimpleClinic.Common.Helpers;

using Microsoft.Extensions.Configuration;

using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

/// <summary>
/// Sending emails in different case scenarios
/// </summary>
public class EmailService
{
    private readonly IConfiguration configuration;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="configuration"></param>
    public EmailService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    /// <summary>
    /// send email when booking is canceled, doctor and patient side
    /// </summary>
    /// <param name="email"></param>
    /// <param name="name"></param>
    /// <param name="phone"></param>
    /// <param name="message"></param>
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
