using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.ViewModels
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDonor { get; set; }
    }
}
