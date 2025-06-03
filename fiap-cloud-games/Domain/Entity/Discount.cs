using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entity
{
    public class Discount : EntityBase
    {
        public int IdGame { get; set; }

        private DateTime _startDiscount 
        { 
            set
            { 
                _startDiscount = value; 
            }
        }
        public DateTime StartDiscount { get; }

        private DateTime _endDiscount
        {
            set
            {
                _endDiscount = value;
            }
        }
        public DateTime EndDiscount { get; }

        private float _percentOff;
        public float PercentOff
        {
            get => _percentOff;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Discount PercentOff must be at least 0.01.");

                if (value > 0.9)
                    throw new ArgumentException("Discount PercentOff must be a maximum of 0.9.");

                _percentOff = value;
            }
        }

        [NotMapped]
        public decimal DiscountedPrice { get; set; }

        public Game Game { get; set; }

        override public bool Status { get; set; } = true;

        public void SetDiscountPeriod(DateTime startDiscount, DateTime endDiscount)
        {
            if (startDiscount < new DateTime(1753, 1, 1))
                throw new ArgumentException("Minimum value for StartDiscount date is 01/01/1753.");

            if (endDiscount < new DateTime(1753, 1, 1))
                throw new ArgumentException("Minimum value for EndDiscount date is 01/01/1753.");

            if (endDiscount <= startDiscount)
                throw new ArgumentException("The EndDiscount date must be greater than the StartDiscount date.");

            _startDiscount = startDiscount;
            _endDiscount = endDiscount;
        }
    }
}
