using LoggerRegistration.Data;
using LoggerRegistration.Interface;
using LoggerRegistration.Models;
using MongoDB.Driver;

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

            string registrationId = $"customLogger-{registration.CompanyName}-{registration.ProjectName}-{registration.ServiceName}-{Guid.NewGuid()}";
            // Save the registration to the database
            registration.RegistrationId = registrationId.ToLower();
            _dbContext.Registrations.InsertOne(registration);
            return registrationId;
        }

        public bool IsRegistrationIdValid(string registrationId)
        {
            var filter = Builders<Registration>.Filter.Eq(r => r.RegistrationId, registrationId);
            var registration = _dbContext.Registrations.Find(filter).FirstOrDefault();

            return registration != null;
        }
    }
}
