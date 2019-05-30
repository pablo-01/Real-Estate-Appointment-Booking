﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BookingWebsite.Service
{
    public class EmailSender : IEmailSender
    {

        public EmailOptions Options { get; set; }

        // dependency injection
        public EmailSender(IOptions<EmailOptions> emailOptions)
        {
            Options = emailOptions.Value;

        }
        

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        private Task Execute(string sendGridKey, string subject, string message, string email)
        {

            var client = new SendGridClient(sendGridKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("open@properties.com", "Open Properties LTD"),
                Subject = subject,
                HtmlContent = message
            };

            msg.AddTo(new EmailAddress(email));
            try
            {
                return client.SendEmailAsync(msg);
            }
            catch(Exception ex)
            {

            }

            return null;
        }
    }
}
