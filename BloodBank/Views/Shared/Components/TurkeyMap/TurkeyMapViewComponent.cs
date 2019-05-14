using BloodBank.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Views.Shared.Components.BootstrapPopup
{
    public class PaymentSuccessfulViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public PaymentSuccessfulViewComponent(ApplicationDbContext dbContext)
        {
            this._context = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
