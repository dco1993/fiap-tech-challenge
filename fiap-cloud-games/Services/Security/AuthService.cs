using Infra.Data.Repository;
using Services.DTO;
using Services.Mappers;

namespace Services.Security
{
    public class AuthService
    {
        UserRepository _userRepository;
        TokenService _tokenService;

        public AuthService(UserRepository userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public string Login(UserLoginDTO login)
        {
            var user = _userRepository.GetByEmail(login.Email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found.");
            }

            if (user.Email != login.Email || user.Password != login.Password)
            {
                throw new UnauthorizedAccessException("Invalid login information.");
            }

            return _tokenService.GenerateToken(user.Email, user.AccessLevel.Name);
        }

        public UserCreatedDTO CreateUser(UserCreateDTO newUser)
        {
            var user = _userRepository.GetByEmail(newUser.Email);

            if (user != null)
            {
                throw new ArgumentException("Email already registered.");
            }

            var userCreated = _userRepository.Add(UserMapper.ToEntity(newUser));

            return UserMapper.ToUserCreated(userCreated);
        }
    }
}
