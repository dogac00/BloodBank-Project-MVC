using System;
using System.Collections.Generic;
using System.Text;
using BloodBank.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Data
{
    public class ApplicationDbContext : IdentityDbContext<BloodBankUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BloodBank.Models.Post> Posts { get; set; }
        public DbSet<BloodBank.Models.CityPoints> CityPoints { get; set; }
        public DbSet<BloodBank.Models.Donation> Donations { get; set; }
        public DbSet<BloodBank.Models.Conversation> Conversations { get; set; }
        public DbSet<BloodBank.Models.Message> Messages { get; set; }
        public DbSet<BloodBank.Models.Location> Locations { get; set; }
    }
}
