using AADP.Application.Port.In;
using AADP.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace AADP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController(ILogInServices logInServices, ILogger<LogInController> logger) : ControllerBase
    {
        private readonly ILogInServices _logInServices = logInServices;

        private readonly ILogger<LogInController> _logger = logger;

        [HttpPost("GenerateToken")]
        public async Task<ActionResult<Token>> GenerateTokenAsync(UserCredential userCredential)
        {
            try
            {
                return Ok(await _logInServices.GenerateTokenAsync(userCredential));
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt in GenerateTokenAsync for email: {Email}", userCredential.Email);
                return Unauthorized("Invalid email or password.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating token in GenerateTokenAsync for email: {Email}", userCredential.Email);
                return Problem("An error occurred while processing the request.");
            }
        }
    }
}
