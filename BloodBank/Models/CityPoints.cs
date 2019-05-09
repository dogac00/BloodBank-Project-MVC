using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Models
{
    public class CityPoints
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
