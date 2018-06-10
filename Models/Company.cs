using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crmApi.Models
{
    public class Company
    {
        public long Id { get; set; }
        public string CompanyName { get; set; }
        public Nullable<long> Sales { get; set; }
        public Nullable<long> Employees { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public bool Contact { get; set; }
        public bool Mail { get; set; }
        public DateTime MailDate { get; set; }
        public DateTime CallOn { get; set; }
        public string CompanyNotes { get; set; }
        public Nullable<int> Rating { get; set; }
        public bool Nearby { get; set; }
        public Nullable<int> Appropriate { get; set; }
        public bool Consultants { get; set; }

        public List<Contact> Contacts { get; set; }
    }
}
