using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloodBank.Data;
using BloodBank.Models;
using BloodBank.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace BloodBank.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BloodBankUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private UserManagementController _userManagement;

        public PostsController(ApplicationDbContext context, UserManager<BloodBankUser> userManager, 
            RoleManager<IdentityRole> roleManager, UserManagementController userManagement)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _userManagement = userManagement;
        }
        
        public async Task<IActionResult> Index()
        {
            var postList = _context
                .Posts
                .Include("Owner")
                .ToList();

            var postModel = new PostIndexModel();

            foreach (var item in postList)
            {
                var post = new Post
                {
                    Id = item.Id,
                    Title = item.Title,
                    BloodType = item.BloodType,
                    City = item.City,
                    Category = item.Category,
                    Description = item.Description,
                    Owner = item.Owner
                };

                postModel.Posts.Add(post);
            }

            var _currentUser = GetCurrentUser();

            postModel.IsAdmin = _currentUser == null ? false : await _userManager.IsInRoleAsync(_currentUser, "admin");
            postModel.IsDonor = _currentUser == null ? false : _currentUser.IsDonor;

            return View(postModel);
        }

        public BloodBankUser GetCurrentUser()
        {
            if (User == null) return null;

            var userName = User.Identity.Name;
            var _currentUser = userName == null ? null : _userManager.FindByNameAsync(User.Identity.Name).Result;

            return _currentUser;
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,BloodType,City,Description,Category")] Post post)
        {
            post.Owner = GetCurrentUser();

            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,BloodType,City,Description,Category")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
