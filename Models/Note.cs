using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace crmApi.Models
{
    public class Note
    {
        [Key]
        public long Id { get; set; }
        public long ContactId { get; set; }
        public DateTime ContactDate { get; set; }
        public string NoteText { get; set; }

        public Contact Contact { get; set; }
    }
}
