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
    [Route("api/SalesModels")]
    public class SalesModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SalesModels
        [HttpGet]
        public IEnumerable<SalesModel> GetSalesModel()
        {
            return _context.SalesModel;
        }

        // GET: api/SalesModels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalesModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salesModel = await _context.SalesModel.SingleOrDefaultAsync(m => m.Sales_id == id);

            if (salesModel == null)
            {
                return NotFound();
            }

            return Ok(salesModel);
        }

        // PUT: api/SalesModels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesModel([FromRoute] int id, [FromBody] SalesModel salesModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesModel.Sales_id)
            {
                return BadRequest();
            }

            _context.Entry(salesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesModelExists(id))
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

        // POST: api/SalesModels
        [HttpPost]
        public async Task<IActionResult> PostSalesModel([FromBody] SalesModel salesModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SalesModel.Add(salesModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalesModel", new { id = salesModel.Sales_id }, salesModel);
        }

        // DELETE: api/SalesModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salesModel = await _context.SalesModel.SingleOrDefaultAsync(m => m.Sales_id == id);
            if (salesModel == null)
            {
                return NotFound();
            }

            _context.SalesModel.Remove(salesModel);
            await _context.SaveChangesAsync();

            return Ok(salesModel);
        }

        private bool SalesModelExists(int id)
        {
            return _context.SalesModel.Any(e => e.Sales_id == id);
        }
    }
}