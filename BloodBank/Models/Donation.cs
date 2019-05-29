using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string NameOnCard { get; set; }
        public long CreditCardNumber { get; set; }
        public int ExpireDateMonth { get; set; }
        public int ExpireDateYear { get; set; }
    }
}
