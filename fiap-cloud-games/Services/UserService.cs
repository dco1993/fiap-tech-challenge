using Infra.Data.Repository;
using Services.DTO;
using Services.Mappers;

namespace Services
{
    public class UserService
    {
        UserRepository _userRepository;
        AccessLevelRepository _accessLevelRepository;

        public UserService(UserRepository userRepository, AccessLevelRepository accessLevelRepository)
        {
            _userRepository = userRepository;
            _accessLevelRepository = accessLevelRepository;
        }

        public UserAccessLevelDTO UpdateAccessLevel(int idUser, int idAccessLevel, string loggedUser)
        {
            var user = _userRepository.GetById(idUser);

            if (user is null)
                throw new ArgumentException("User not found.");

            if (user.Name == "Admin")
                throw new ArgumentException("Can`t change Admin access level.");

            if (user.Email == loggedUser)
                throw new ArgumentException("This change cannot be made on the user logged on.");

            var accessLevel = _accessLevelRepository.GetById(idAccessLevel);

            if (accessLevel is null)
                throw new ArgumentException("Access vevel not found.");

            user = _userRepository.UpdateAccessLevel(user.Id, accessLevel.Id);

            return UserMapper.ToUserAccessLevelDTO(user);
        }

        public List<UserDTO> GetAllUsers()
        {
            var users = _userRepository.GetAll().ToList();

            if (users is null || !users.Any())
                throw new ApplicationException("No users found.");

            return users.Select(u => UserMapper.ToUserDTO(u)).ToList();
        }
    }
}
