using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBank.Data;
using BloodBank.Models;
using BloodBank.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BloodBank.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BloodBankUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(ApplicationDbContext context, UserManager<BloodBankUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // GET: UserManagement
        public async Task<ActionResult> Index()
        {
            var userList = _context
                .Users
                .ToList();

            List<UserModel> userModelList = new List<UserModel>();

            foreach (var item in userList)
            {
                bool isadmin = await _userManager.IsInRoleAsync(item, "admin");

                var user = new UserModel
                {
                    Id = item.Id,
                    FullName = item.FirstName + " " + item.LastName,
                    Mail = item.Email,
                    Password = item.PasswordHash,
                    IsAdmin = isadmin,
                    IsDonor = item.IsDonor
                };

                userModelList.Add(user);
            }

            return View(userModelList);
        }

        public BloodBankUser GetCurrentUser()
        {
            if (User == null) return null;

            var userName = User.Identity.Name;
            var _currentUser = userName == null ? null : _userManager.FindByNameAsync(User.Identity.Name).Result;

            return _currentUser;
        }

        public BloodBankUser GetUserByName(string FirstName)
        {
            return _context.Users.FirstOrDefault(u => u.FirstName == FirstName);
        }

        public async Task<bool> IsAdmin()
        {
            var userName = User.Identity.Name;
            return userName == null ? false : await _userManager.IsInRoleAsync(_userManager.FindByNameAsync(userName).Result, "admin");
        }

        // GET: UserManagement/Details/5
        public async Task<ActionResult> MakeAdmin(string id)
        {
            if (!(await _roleManager.RoleExistsAsync("admin")))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "admin" });
            }

            var user = await _userManager.FindByIdAsync(id);
            await _userManager.AddToRoleAsync(user, "admin");
            return RedirectToAction("index");
        }

        public async Task<ActionResult> RemoveAdmin(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.RemoveFromRoleAsync(user, "admin");
            return RedirectToAction("index");
        }
    }
}