using LoggerRegistration.Data;
using LoggerRegistration.Interface;
using LoggerRegistration.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LoggerRegistration.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(IRegistrationService registrationService, ILogger<RegistrationController> logger)
        {
            _registrationService = registrationService;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<Registration> CreateRegistration(RegistrationModel model)
        {
            var registration = new Registration
            {
                CompanyName = model.CompanyName,
                ProjectName = model.ProjectName,
                UserName = model.UserName,
                ServiceName = model.ServiceName,
                CreatedAt = DateTime.UtcNow
            };

            registration.RegistrationId = _registrationService.GenerateRegistrationId(registration);

            // Save the registration to the database or perform any other required actions

            return Ok(new { RegistrationId = registration.RegistrationId });
        }

        [HttpGet("{registrationId}")]
        public ActionResult<bool> IsRegistrationIdValid(string registrationId)
        {
            var isValid = _registrationService.IsRegistrationIdValid(registrationId);

            return Ok(isValid);
        }
    }
}
