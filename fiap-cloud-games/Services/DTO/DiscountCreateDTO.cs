using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class DiscountCreateDTO
    {
        public int IdGame { get; set; }
        public DateTime StartDiscount { get; set; }
        public DateTime EndDiscount { get; set; }
        public float DiscountPercentage { get; set; }
    }
}
