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
    public class ProductsModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductsModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductsModel.Include(p => p.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductsModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productsModel = await _context.ProductsModel
                .Include(p => p.ApplicationUser)
                .SingleOrDefaultAsync(m => m.Product_id == id);
            if (productsModel == null)
            {
                return NotFound();
            }

            return View(productsModel);
        }

        // GET: ProductsModels/Create
        public IActionResult Create()
        {
            ViewData["User_id"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ProductsModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Product_id,Name,Price,Description,Product_tag,User_id")] ProductsModel productsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["User_id"] = new SelectList(_context.Users, "Id", "Id", productsModel.User_id);
            return View(productsModel);
        }

        // GET: ProductsModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productsModel = await _context.ProductsModel.SingleOrDefaultAsync(m => m.Product_id == id);
            if (productsModel == null)
            {
                return NotFound();
            }
            ViewData["User_id"] = new SelectList(_context.Users, "Id", "Id", productsModel.User_id);
            return View(productsModel);
        }

        // POST: ProductsModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Product_id,Name,Price,Description,Product_tag,User_id")] ProductsModel productsModel)
        {
            if (id != productsModel.Product_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsModelExists(productsModel.Product_id))
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
            ViewData["User_id"] = new SelectList(_context.Users, "Id", "Id", productsModel.User_id);
            return View(productsModel);
        }

        // GET: ProductsModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productsModel = await _context.ProductsModel
                .Include(p => p.ApplicationUser)
                .SingleOrDefaultAsync(m => m.Product_id == id);
            if (productsModel == null)
            {
                return NotFound();
            }

            return View(productsModel);
        }

        // POST: ProductsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productsModel = await _context.ProductsModel.SingleOrDefaultAsync(m => m.Product_id == id);
            _context.ProductsModel.Remove(productsModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsModelExists(int id)
        {
            return _context.ProductsModel.Any(e => e.Product_id == id);
        }
    }
}
