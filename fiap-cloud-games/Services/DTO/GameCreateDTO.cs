using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class GameCreateDTO
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Metacritic { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string About { get; set; }
        public decimal Price { get; set; }
    }
}
