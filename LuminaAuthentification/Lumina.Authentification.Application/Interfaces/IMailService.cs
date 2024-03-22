

using Lumina.Authentification.Application.Commons.Email;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Lumina.Authentification.Application.Interfaces
{
    public interface IMailService
    {
        bool SendMail(MailData mailData);
    }


    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettingsOptions)
        {
            _mailSettings = mailSettingsOptions.Value;
        }

        public bool SendMail(MailData mailData)
        {
            try
            {
                string fromMail = _mailSettings.SenderEmail;
                string fromPassword = _mailSettings.Password;

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = mailData.EmailSubject;
                message.To.Add(new MailAddress(mailData.EmailToName));
                message.Body = mailData.EmailBody;
                message.IsBodyHtml = true;

                var smtpClient = new System.Net.Mail.SmtpClient(_mailSettings.Server)
                {
                    Port = _mailSettings.Port,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                };

                smtpClient.Send(message);
                Console.WriteLine("Mail sent successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }


        }
    }
}
