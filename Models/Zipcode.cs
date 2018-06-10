using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crmApi.Models
{
    public class Zipcode
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public int Proximity { get; set; }

    }
}
