using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class UserGameCreateDTO
    {
        public string UserEmail { get; set; }
        public int IdGame { get; set; }
        public int IdUser { get; set; }
    }
}
