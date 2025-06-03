using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class UserGame : EntityBase
    {
        public int IdUser { get; set; }
        public User User { get; set; }

        public int IdGame { get; set; }
        public Game Game { get; set; }

        override public bool Status { get; set; } = true;
    }
}
