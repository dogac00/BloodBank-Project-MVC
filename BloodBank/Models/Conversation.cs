using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Models
{
    public class Conversation
    {
        public int ConversationId { get; set; }
        public string PostOwnerName { get; set; }
        public string ApplierName { get; set; }
        public virtual IList<Message> Messages { get; set; }
    }
}
