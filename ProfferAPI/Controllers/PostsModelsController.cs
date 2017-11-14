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

namespace ProfferAPI.Controllers
{
    [Authorize]
    public class PostsModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PostsModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PostsModel.Include(p => p.ApplicationUser).Include(p => p.ProductsModel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PostsModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postsModel = await _context.PostsModel
                .Include(p => p.ApplicationUser)
                .Include(p => p.ProductsModel)
                .SingleOrDefaultAsync(m => m.Posts_id == id);
            if (postsModel == null)
            {
                return NotFound();
            }

            return View(postsModel);
        }

        // GET: PostsModels/Create
        public IActionResult Create()
        {
            ViewData["User_id"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["Product_id"] = new SelectList(_context.ProductsModel, "Product_id", "User_id");
            return View();
        }

        // POST: PostsModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Posts_id,Product_id,User_id")] PostsModel postsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["User_id"] = new SelectList(_context.Users, "Id", "Id", postsModel.User_id);
            ViewData["Product_id"] = new SelectList(_context.ProductsModel, "Product_id", "User_id", postsModel.Product_id);
            return View(postsModel);
        }

        // GET: PostsModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postsModel = await _context.PostsModel.SingleOrDefaultAsync(m => m.Posts_id == id);
            if (postsModel == null)
            {
                return NotFound();
            }
            ViewData["User_id"] = new SelectList(_context.Users, "Id", "Id", postsModel.User_id);
            ViewData["Product_id"] = new SelectList(_context.ProductsModel, "Product_id", "User_id", postsModel.Product_id);
            return View(postsModel);
        }

        // POST: PostsModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Posts_id,Product_id,User_id")] PostsModel postsModel)
        {
            if (id != postsModel.Posts_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostsModelExists(postsModel.Posts_id))
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
            ViewData["User_id"] = new SelectList(_context.Users, "Id", "Id", postsModel.User_id);
            ViewData["Product_id"] = new SelectList(_context.ProductsModel, "Product_id", "User_id", postsModel.Product_id);
            return View(postsModel);
        }

        // GET: PostsModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postsModel = await _context.PostsModel
                .Include(p => p.ApplicationUser)
                .Include(p => p.ProductsModel)
                .SingleOrDefaultAsync(m => m.Posts_id == id);
            if (postsModel == null)
            {
                return NotFound();
            }

            return View(postsModel);
        }

        // POST: PostsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postsModel = await _context.PostsModel.SingleOrDefaultAsync(m => m.Posts_id == id);
            _context.PostsModel.Remove(postsModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostsModelExists(int id)
        {
            return _context.PostsModel.Any(e => e.Posts_id == id);
        }
    }
}
