using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfferAPI.Data;
using ProfferAPI.Models;

namespace ProfferAPI.Controllers.API
{
    [Produces("application/json")]
    [Route("api/PostsModels")]
    public class PostsModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PostsModels
        [HttpGet]
        public IEnumerable<PostsModel> GetPostsModel()
        {
            return _context.PostsModel;
        }

        // GET: api/PostsModels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostsModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postsModel = await _context.PostsModel.SingleOrDefaultAsync(m => m.Posts_id == id);

            if (postsModel == null)
            {
                return NotFound();
            }

            return Ok(postsModel);
        }

        // PUT: api/PostsModels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostsModel([FromRoute] int id, [FromBody] PostsModel postsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != postsModel.Posts_id)
            {
                return BadRequest();
            }

            _context.Entry(postsModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostsModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PostsModels
        [HttpPost]
        public async Task<IActionResult> PostPostsModel([FromBody] PostsModel postsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PostsModel.Add(postsModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPostsModel", new { id = postsModel.Posts_id }, postsModel);
        }

        // DELETE: api/PostsModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostsModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postsModel = await _context.PostsModel.SingleOrDefaultAsync(m => m.Posts_id == id);
            if (postsModel == null)
            {
                return NotFound();
            }

            _context.PostsModel.Remove(postsModel);
            await _context.SaveChangesAsync();

            return Ok(postsModel);
        }

        private bool PostsModelExists(int id)
        {
            return _context.PostsModel.Any(e => e.Posts_id == id);
        }
    }
}