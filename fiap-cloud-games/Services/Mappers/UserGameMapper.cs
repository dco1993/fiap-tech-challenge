using Domain.Entity;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mappers
{
    internal class UserGameMapper
    {
        internal static UserGame ToEntity(UserGameCreateDTO userGame)
        {
            return new UserGame
            {
                IdUser = userGame.IdUser,
                IdGame = userGame.IdGame,
            };
        }

        internal static UserGameCreatedDTO ToDTO(UserGame userGame)
        {
            return new UserGameCreatedDTO
            {
                Name = userGame.User.Name,
                Email = userGame.User.Email,
                GameTitle = userGame.Game.Title
            };
        }

    }
}
