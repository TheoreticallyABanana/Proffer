using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProfferAPI.Data;
using ProfferAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ProfferAPI.Controllers
{
    [Authorize]
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
            var applicationDbContext = _context.SalesModel.Include(s => s.ApplicationUser).Include(s => s.ProductsModel);
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
                .Include(s => s.ProductsModel)
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
            ViewData["User_id"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["Product_id"] = new SelectList(_context.ProductsModel, "Product_id", "User_id");
            return View();
        }

        // POST: SalesModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sales_id,Sales_price,Date_sold,User_id,Product_id")] SalesModel salesModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["User_id"] = new SelectList(_context.ApplicationUser, "Id", "Id", salesModel.User_id);
            ViewData["Product_id"] = new SelectList(_context.ProductsModel, "Product_id", "User_id", salesModel.Product_id);
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
            ViewData["User_id"] = new SelectList(_context.ApplicationUser, "Id", "Id", salesModel.User_id);
            ViewData["Product_id"] = new SelectList(_context.ProductsModel, "Product_id", "User_id", salesModel.Product_id);
            return View(salesModel);
        }

        // POST: SalesModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Sales_id,Sales_price,Date_sold,User_id,Product_id")] SalesModel salesModel)
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
            ViewData["User_id"] = new SelectList(_context.ApplicationUser, "Id", "Id", salesModel.User_id);
            ViewData["Product_id"] = new SelectList(_context.ProductsModel, "Product_id", "User_id", salesModel.Product_id);
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
                .Include(s => s.ProductsModel)
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
