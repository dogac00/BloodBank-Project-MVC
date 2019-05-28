using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.ViewModels
{
    public class ConversationViewModel
    {
        public Conversation Conversation { get; set; }
        public bool IsNull { get; set; }
    }
}
