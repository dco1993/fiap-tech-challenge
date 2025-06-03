using Microsoft.AspNetCore.Mvc;
using Services.Security;
using Services;
using Services.DTO;
using Domain.Entity;

namespace App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        private LogService<AuthController> _log;
        private AuthService _authService;

        public AuthController(LogService<AuthController> log, AuthService authService)
        {
            _log = log;
            _authService = authService;
        }

        [HttpPost("getToken")]
        public IActionResult GetToken(UserLoginDTO login)
        {
            try
            {
                _log.LogInformation($"Attempting login for user: {login.Email}");

                var accessToken = _authService.Login(login);

                _log.LogInformation($"User {login.Email} authenticated.");

                return Ok(new { responseCode = 200, accessToken });
            }
            catch (UnauthorizedAccessException ex)
            {
                _log.LogError($"Authentication error for {login.Email}. Message: {ex.Message}");

                return Unauthorized(new { responseCode = 401, message = ex.Message });
            }
        }

        [HttpPost("createUser")]
        public IActionResult createUser(UserCreateDTO newUser)
        {
            try
            {
                _log.LogInformation($"User creation request: {newUser.Email}");

                var data = _authService.CreateUser(newUser);

                _log.LogInformation($"User created successfully: {newUser.Email}");

                return Ok(new { responseCode = 200, message = "User created successfully.", data });
            }
            catch (ArgumentException ex)
            {
                _log.LogError($"User creation failed for {newUser.Email}. Message: {ex.Message}");

                return BadRequest(new { responseCode = 400, message = ex.Message });
            }
        }

    }
}
