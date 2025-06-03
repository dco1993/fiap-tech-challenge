using Domain.Entity;
using Services.DTO;

namespace Services.Mappers
{
    internal static class UserMapper
    {
        internal static User ToEntity(UserCreateDTO newUser)
        {
            return new User
            {
                Name = newUser.Name,
                Email = newUser.Email,
                Password = newUser.Password
            };
        }

        internal static User ToEntity(UserLoginDTO userLogin)
        {
            return new User
            {
                Email = userLogin.Email,
                Password = userLogin.Password
            };
        }

        internal static UserCreatedDTO ToUserCreated(User createdUser)
        {
            return new UserCreatedDTO
            {
                Name = createdUser.Name,
                Email = createdUser.Email
            };
        }

        internal static UserAccessLevelDTO ToUserAccessLevelDTO(User user)
        {
            return new UserAccessLevelDTO
            {
                Name = user.Name,
                Email = user.Email,
                AccessLevel = user.AccessLevel.Name
            };
        }

        internal static UserDTO ToUserDTO(User user)
        {
            return new UserDTO
            {
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
