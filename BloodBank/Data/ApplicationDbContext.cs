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
        public DbSet<BloodBank.Models.Post> Post { get; set; }
    }
}
