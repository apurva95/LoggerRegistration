using LoggerRegistration.Data;
using LoggerRegistration.Interface;
using LoggerRegistration.Models;
using MimeKit;
using MongoDB.Driver;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace LoggerRegistration.Service
{
    public class RegistrationService : IRegistrationService
    {
        private readonly LoggerRegistrationDbContext _dbContext;
        public RegistrationService(LoggerRegistrationDbContext database)
        {
            _dbContext = database;
        }
        public string GenerateRegistrationId(Registration registration)
        {
            // Generate a unique registration ID based on the provided registration details
            // You can use any logic to generate the ID, such as combining fields or using GUIDs

            string registrationId = !string.IsNullOrEmpty(registration.ServiceName) ? $"customlogger-{registration.CompanyName}-{registration.ProjectName}-{registration.ServiceName}-{Guid.NewGuid()}" : $"customlogger-{registration.CompanyName}-{registration.ProjectName}-{Guid.NewGuid()}";
            // Save the registration to the database
            registration.RegistrationId = registrationId.ToLower();
            _dbContext.Registrations.InsertOne(registration);
            try
            {
                SendEmail(registration.EmailId, "Registration Confirmation!", $"Your Registration Id: {registration.RegistrationId}.\n Please keep this for future refrence and to see logs.");
            }
            catch (Exception ex)
            {

            }
            return registrationId;
        }

        public bool IsRegistrationIdValid(string registrationId)
        {
            var filter = Builders<Registration>.Filter.Eq(r => r.RegistrationId, registrationId);
            var registration = _dbContext.Registrations.Find(filter).FirstOrDefault();

            return registration != null;
        }

        private static void SendEmail(string recipientEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Logger Admin", "aptandon11995@gmail.com"));
            message.To.Add(new MailboxAddress("Recipient Name", recipientEmail));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 465, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate("aptandon11995@gmail.com", "affekyhfqwmfgmyr");
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
