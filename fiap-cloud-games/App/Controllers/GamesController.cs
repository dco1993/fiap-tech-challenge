using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.DTO;
using System.Security.Claims;

namespace App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class GamesController : ControllerBase
    {
        private GameService _gameService;

        public GamesController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("getListGames")]
        public IActionResult GetListGames()
        {
            return Ok(new { responseCode = 200, data = _gameService.GetAllWithDiscounts()});
        }

        [HttpPost("addGameToLibrary")]
        [Authorize]
        public IActionResult AddGameToLibrary(int idGame)
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var  newUserGame = new UserGameCreateDTO
            {
                UserEmail = email,
                IdGame = idGame
            };

            var bindResult = _gameService.BindGameToUser(newUserGame);

            return Ok(new { responseCode = 200, message = "Game successfully added to library!", data = bindResult });
        }

        [HttpGet("getMyLibrary")]
        [Authorize]
        public IActionResult GetMyLibrary()
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return Ok(new { responseCode = 200, data = _gameService.GetLibrary(email) });
        }
    }
}
