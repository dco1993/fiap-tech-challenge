using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CorrelationId
    {
        private static string _correlationId;

        public string Get() => _correlationId;

        public void Set(string correlationId) => _correlationId = correlationId;
    }
}
