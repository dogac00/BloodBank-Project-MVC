using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BloodType { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public BloodBankUser Owner { get; set; }
    }
}
