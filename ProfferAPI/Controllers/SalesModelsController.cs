using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProfferAPI.Data;
using ProfferAPI.Models;

namespace ProfferAPI.Controllers
{
    public class SalesModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SalesModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SalesModel.Include(s => s.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SalesModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesModel = await _context.SalesModel
                .Include(s => s.ApplicationUser)
                .SingleOrDefaultAsync(m => m.Sales_id == id);
            if (salesModel == null)
            {
                return NotFound();
            }

            return View(salesModel);
        }

        // GET: SalesModels/Create
        public IActionResult Create()
        {
            ViewData["User_id"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: SalesModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sales_id,Offer_price,Sales_price,User_id,Status")] SalesModel salesModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["User_id"] = new SelectList(_context.Users, "Id", "Id", salesModel.User_id);
            return View(salesModel);
        }

        // GET: SalesModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesModel = await _context.SalesModel.SingleOrDefaultAsync(m => m.Sales_id == id);
            if (salesModel == null)
            {
                return NotFound();
            }
            ViewData["User_id"] = new SelectList(_context.Users, "Id", "Id", salesModel.User_id);
            return View(salesModel);
        }

        // POST: SalesModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Sales_id,Offer_price,Sales_price,User_id,Status")] SalesModel salesModel)
        {
            if (id != salesModel.Sales_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesModelExists(salesModel.Sales_id))
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
            ViewData["User_id"] = new SelectList(_context.Users, "Id", "Id", salesModel.User_id);
            return View(salesModel);
        }

        // GET: SalesModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesModel = await _context.SalesModel
                .Include(s => s.ApplicationUser)
                .SingleOrDefaultAsync(m => m.Sales_id == id);
            if (salesModel == null)
            {
                return NotFound();
            }

            return View(salesModel);
        }

        // POST: SalesModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesModel = await _context.SalesModel.SingleOrDefaultAsync(m => m.Sales_id == id);
            _context.SalesModel.Remove(salesModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesModelExists(int id)
        {
            return _context.SalesModel.Any(e => e.Sales_id == id);
        }
    }
}
