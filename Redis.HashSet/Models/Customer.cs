using System;
using System.Collections.Generic;
using System.Text;

namespace Redis.HashSet.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public long AccountNumber { get; set; }
        public DateTime BirthDay { get; set; }
        public CustomerType Type { get; set; }
        public int? SecurityNumber { get; set; }
        public bool Authenticated { get; set; }
        public List<Address> Addresses { get; set; }
    }

    public enum CustomerType
    {
        Normal,
        Premium,
        Plus
    }
}
