using Infra.Data.Repository;
using Services.DTO;
using Services.Mappers;

namespace Services
{
    public class GameService
    {
        private GameRepository _gameRepository;
        private UserRepository _userRepository;
        private UserGameRepository _userGameRepository;

        public GameService(GameRepository gameRepository, UserRepository userRepository, UserGameRepository userGameRepository )
        {
            _gameRepository = gameRepository;
            _userRepository = userRepository;
            _userGameRepository = userGameRepository;
        }

        public string CreateGame(GameCreateDTO newGame)
        {
            var gameCreated = _gameRepository.Add(GameMapper.ToEntity(newGame));

            return gameCreated.Title;
        }

        public List<GameDTO> GetAllWithDiscounts()
        {
            var games = _gameRepository.GetAllWithDiscounts();

            var gameDTO = games.Select(g => GameMapper.ToDTO(g)).ToList();

            return gameDTO;
        }

        public UserGameCreatedDTO BindGameToUser(UserGameCreateDTO newBind)
        {
            var user = _userRepository.GetByEmail(newBind.UserEmail);
            var game = _gameRepository.GetById(newBind.IdGame);

            if (user is null)
                throw new ArgumentException("User not found.");

            if (game is null)
                throw new ArgumentException("Game not found.");

            newBind.IdUser = user.Id;
            var addedBind = _userGameRepository.Add(UserGameMapper.ToEntity(newBind));

            if (addedBind is null)
                throw new ApplicationException("Error adding game to user library.");

            addedBind.User = user;
            addedBind.Game = game;

            return UserGameMapper.ToDTO(addedBind);
        }

        public List<GameLibraryDTO> GetLibrary(string email)
        {
            var userLibrary = _gameRepository.GetLibrary(email);
            var gameDTO = userLibrary.Select(g => GameMapper.ToLibraryDTO(g)).ToList();
            return gameDTO;
        }

    }
}
