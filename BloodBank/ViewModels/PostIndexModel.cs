using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.ViewModels
{
    public class PostIndexModel
    {
        public List<Post> Posts { get; set; } = new List<Post>();
        public bool IsAdmin { get; set; }
        public bool IsDonor { get; set; }
    }
}
