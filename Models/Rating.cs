using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace crmApi.Models
{
    public class Rating
    {
        [Key]
        public long Id { get; set; }
        public int RatingValue { get; set; }
        public string Description { get; set; }
    }
}
