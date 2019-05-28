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
    public class CityPointsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CityPointsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CityPoints
        public async Task<IActionResult> Index(int? id)
        {
            ViewData["route-id"] = id;
            return View(await _context.CityPoints.Where(c => c.CityId == id).ToListAsync());
        }

        // GET: CityPoints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cityPoints = await _context.CityPoints
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cityPoints == null)
            {
                return NotFound();
            }

            return View(cityPoints);
        }

        // GET: CityPoints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CityPoints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CityId,Title,Address,Phone")] CityPoints cityPoints)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cityPoints);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cityPoints);
        }

        // GET: CityPoints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cityPoints = await _context.CityPoints.FindAsync(id);
            if (cityPoints == null)
            {
                return NotFound();
            }
            return View(cityPoints);
        }

        // POST: CityPoints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CityId,Title,Address,Phone")] CityPoints cityPoints)
        {
            if (id != cityPoints.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cityPoints);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityPointsExists(cityPoints.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cityPoints);
        }

        // GET: CityPoints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cityPoints = await _context.CityPoints
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cityPoints == null)
            {
                return NotFound();
            }

            return View(cityPoints);
        }

        // POST: CityPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cityPoints = await _context.CityPoints.FindAsync(id);
            _context.CityPoints.Remove(cityPoints);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityPointsExists(int id)
        {
            return _context.CityPoints.Any(e => e.Id == id);
        }
    }
}
