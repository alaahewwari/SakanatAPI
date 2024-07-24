﻿using BusinessLogic.Helpers;
using BusinessLogic.Infrastructure.Email;
using BusinessLogic.Services.EmailServices.Interfaces;
using MailKit.Security;
using MimeKit;

namespace BusinessLogic.Services.EmailServices.Implementations;

public class EmailServices(EmailConfiguration emailConfiguration) : IEmailServices
{

    /// <summary>
    /// A method takes the MimeMessage object and sends it to the SMTP server
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private async Task Send(MimeMessage message)
    {
        using var smtpClient = new MailKit.Net.Smtp.SmtpClient();
        try
        {
            // Establish a secure connection to SMTP server and authenticate
            await smtpClient.ConnectAsync(
                emailConfiguration.SmtpServer,
                emailConfiguration.Port,
                SecureSocketOptions.StartTls);
            smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
            await smtpClient.AuthenticateAsync(emailConfiguration.UserName, emailConfiguration.Password);

            // Send Email
            await smtpClient.SendAsync(message);
        }
        finally
        {
            await smtpClient.DisconnectAsync(true);
        }
    }

    // A method to prepare the MimeMessage object
    private MimeMessage CreateMimeMessage(
        List<string> recipients,
        string subject,
        string content)
    {
        // Convert the list of strings to a list of MailboxAddress objects
        var to = new List<MailboxAddress>();

        foreach (var recipient in recipients)
        {
            var name = recipient[..recipient.IndexOf('@')]; // Substring from the start of the string to the first occurrence of '@'
            to.Add(new MailboxAddress(name, recipient));
        }

        var mimeMessage = new MimeMessage
        {
            From = { new MailboxAddress("Sakanat", emailConfiguration.From) },
            Subject = subject,
            Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = content
            },
        };

        mimeMessage.To.AddRange(to);

        return mimeMessage;
    }

    /// <summary>
    /// Another overload of the CreateMimeMessage method, but receives the body as a MimeEntity object
    /// </summary>
    /// <param name="recipients"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <returns></returns>
    private MimeMessage CreateMimeMessage(
               List<string> recipients,
               string subject,
               MimeEntity body)
    {
        // Convert the list of strings to a list of MailboxAddress objects
        var to = new List<MailboxAddress>();

        foreach (var recipient in recipients)
        {
            var name = recipient[..recipient.IndexOf('@')]; // Substring from the start of the string to the first occurrence of '@'
            to.Add(new MailboxAddress(name, recipient));
        }

        var mimeMessage = new MimeMessage
        {
            From = { new MailboxAddress("Sakanat", emailConfiguration.From) },
            Subject = subject,
            Body = body
        };

        mimeMessage.To.AddRange(to);
        return mimeMessage;
    }

    /// <summary>
    /// High-level Easy-to-use method to send an email
    /// </summary>
    /// <param name="to"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <returns></returns>
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var recipients = new List<string> { to };

        // 1. Prepare the message in the form of a MimeMessage object
        var mimeMessage = CreateMimeMessage(recipients, subject, body);

        // 2. Send the message
        await Send(mimeMessage);
    }


    /// <summary>
    /// Another overload of the SendEmailAsync method, but receives the body as a MimeEntity object
    /// </summary>
    /// <param name="to"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task SendEmailAsync(string to, string subject, MimeEntity body)
    {
        var recipients = new List<string> { to };

        // 1. Prepare the message in the form of a MimeMessage object
        var mimeMessage = CreateMimeMessage(recipients, subject, body);

        // 2. Send the message
        await Send(mimeMessage);
    }


    public async Task SendConfirmationEmailAsync(
        string to,
        string confirmationLink)
    {
        var subject = "Confirm Email";
        var body = EmailMessageGenerator.GenerateConfirmEmailMessageBody(confirmationLink);
        await SendEmailAsync(to, subject, body);
    }
    public async Task SendPasswordResetEmailAsync(
        string to,
        string confirmationLink)
    {
        var subject = "Reset Password Email";
        var body = EmailMessageGenerator.GeneratePasswordResetEmailMessageBody(confirmationLink);

        await SendEmailAsync(to, subject, body);
    }
}