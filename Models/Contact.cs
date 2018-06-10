using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crmApi.Models
{
    public class Contact
    {
        public long Id { get; set; }
        [FromQuery(Name="companyId")]
        public long CompanyId { get; set; }
        public string LName { get; set; }
        public string FName { get; set; }
        public string Phone { get; set; }
        public string Ext { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Dept { get; set; }
        public bool ShouldContact { get; set; }
        public DateTime? DateContacted { get; set; }
        public DateTime? DateMailed { get; set; }
        public bool NoChance { get; set; }
        public DateTime? NextCall { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int? Rating { get; set; }
        public bool NetworkingContact { get; set; }

        public Company Company { get; set; }
        public List<Note> Notes { get; set; }
    }
}
