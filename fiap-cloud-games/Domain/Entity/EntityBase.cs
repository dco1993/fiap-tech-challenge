using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class EntityBase 
    {
        public int Id { get; set; }
        public virtual bool Status { get; set; }
        public DateTime DhTimestamp { get; set; }
    }
}