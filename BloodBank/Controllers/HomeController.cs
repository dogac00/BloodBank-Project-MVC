using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BloodBank.Models;
using Microsoft.AspNetCore.Identity;
using BloodBank.Data;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<BloodBankUser> _userManager;
        private ApplicationDbContext _context;

        public HomeController(UserManager<BloodBankUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> DonationCenters()
        {
            var locations = await _context.Locations.ToListAsync();
            return View(locations);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult DonorRegisterToIndex(int isdonor)
        {
            var user = _userManager.GetUserAsync(User).Result;

            if (isdonor == 1) user.IsDonor = true;
            else user.IsDonor = false;

            _context.Users.Update(user);
            _context.SaveChanges();
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DonorRegister()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
