using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class UserGameRepository : AppRepository<UserGame>
    {
        public UserGameRepository(AppDbContext context) : base(context)
        {
        }
    }
}
