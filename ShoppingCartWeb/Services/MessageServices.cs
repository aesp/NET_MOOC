using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ShoppingCartWeb.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public AuthMessageSender(IOptions<AuthMessageSenderOptions> optionsMess, IOptions<SMSOptions> optionsSms)
        {
            OptionsMess = optionsMess.Value;
            OptionsSms = optionsSms.Value;
        }
        public AuthMessageSenderOptions OptionsMess { get; } //set only via Secret Manager
        public SMSOptions OptionsSms { get; }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Execute(OptionsMess.SendGridKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("alexespinog@gmail.com", "Alex Espino"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(msg);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            // Your Account SID from twilio.com/console
            var accountSid = OptionsSms.SMSAccountIdentification;
            // Your Auth Token from twilio.com/console
            var authToken = OptionsSms.SMSAccountPassword;
            TwilioClient.Init(accountSid, authToken);

            var msg = MessageResource.Create(
              to: new PhoneNumber(number),
              from: new PhoneNumber(OptionsSms.SMSAccountFrom),
              body: message);
            return Task.FromResult(0);
        }
    }
}
