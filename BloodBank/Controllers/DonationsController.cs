using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloodBank.Data;
using BloodBank.Models;

namespace BloodBank.Controllers
{
    public class DonationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Donations
        public IActionResult Donation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeDonation([Bind("Id,Amount,NameOnCard,CreditCardNumber,ExpireDateMonth,ExpireDateYear")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Donation));
            }

            return View("~/Views/Donations/Donation.cshtml");
        }
    }
}
