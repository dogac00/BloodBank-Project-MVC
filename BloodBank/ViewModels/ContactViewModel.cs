using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.ViewModels
{
    public class ContactViewModel
    {
        public Conversation Conversation { get; set; }
        public IList<Message> Messages { get; set; }
        public string OtherPerson { get; set; }
    }
}
