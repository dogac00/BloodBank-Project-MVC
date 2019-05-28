using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Content { get; set; }
        public string OwnerName { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
