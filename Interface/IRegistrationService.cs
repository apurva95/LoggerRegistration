using LoggerRegistration.Interface;
using LoggerRegistration.Models;

namespace LoggerRegistration.Interface
{
    public interface IRegistrationService
    {
        string GenerateRegistrationId(Registration registration);
        bool IsRegistrationIdValid(string registrationId);
    }
}
