using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.DTO;
using System.Security.Claims;

namespace App.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class AdministrationController : ControllerBase
    {
        private LogService<AdministrationController> _log;
        private GameService _gameService;
        private DiscountService _discountService;
        private UserService _userService;

        public AdministrationController(LogService<AdministrationController> log, GameService gameService, DiscountService discountService, UserService userService)
        {
            _log = log;
            _gameService = gameService;
            _discountService = discountService;
            _userService = userService;
        }

        [HttpGet("getUsers")]
        [Authorize(Policy = "Administrator")]
        public IActionResult GetUsers()
        {
            return Ok(new { responseCode = 200, data = _userService.GetAllUsers() });
        }

        [HttpGet("getDiscounts")]
        [Authorize(Policy = "Administrator")]
        public IActionResult GetDiscounts()
        {
            return Ok(new { responseCode = 200, data = _discountService.GetAllDiscounts() });
        }

        [HttpPost("createGame")]
        [Authorize(Policy = "Administrator")]
        public IActionResult CreateGame(GameCreateDTO newGame)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var game = _gameService.CreateGame(newGame);

                _log.LogInformation($"Game created successfully by {email}: {game}");

                return Ok(new { responseCode = 200, message = $"Game created successfully: {game}" });
            }
            catch (ArgumentException ex)
            {
                _log.LogError($"Error creating game {newGame.Title}. Message: {ex.Message}");

                return BadRequest(new { responseCode = 400, message = $"Error creating game: {ex.Message}" });
            }
        }

        [HttpPost("createDiscount")]
        [Authorize(Policy = "Administrator")]
        public IActionResult CreateDiscount(DiscountCreateDTO newDiscount)
        {
            try
            {
                return Ok(new { responseCode = 200, message = $"Discount created successfully.", data = _discountService.CreateDiscount(newDiscount) });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { responseCode = 400, message = $"Error creating discount: {ex.Message}" });
            }
        }

        [HttpPut("changeDiscountStatus")]
        [Authorize(Policy = "Administrator")]
        public IActionResult ChangeDiscountStatus(int idDiscount, bool newDiscountStatus)
        {
            try
            {
                return Ok(new { responseCode = 200, message = $"Discount status changed successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { responseCode = 400, message = $"Error changing discount status: {ex.Message}" });
            }
        }

        [HttpPut("changeUserAccessLevel")]
        [Authorize(Policy = "Administrator")]
        public IActionResult ChangeUserAccessLevel(int idUser, int idAccessLevel)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Ok(new { responseCode = 200, message = $"Access Level changed successfully.", data = _userService.UpdateAccessLevel(idUser, idAccessLevel, email) });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { responseCode = 400, message = $"Error changing access level: {ex.Message}" });
            }
        }
    }
}
