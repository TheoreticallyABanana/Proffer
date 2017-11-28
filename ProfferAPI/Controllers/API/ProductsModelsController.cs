using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfferAPI.Data;
using ProfferAPI.Models;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ProfferAPI.Controllers.API
{
    [Produces("application/json")]
    [Route("api/ProductsModels")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductsModels
        [HttpGet]
        public IEnumerable<ProductsModel> GetProductsModel()
        {
            return _context.ProductsModel;
        }

        // GET: api/ProductsModels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductsModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productsModel = await _context.ProductsModel.SingleOrDefaultAsync(m => m.Product_id == id);

            if (productsModel == null)
            {
                return NotFound();
            }

            return Ok(productsModel);
        }

        // PUT: api/ProductsModels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductsModel([FromRoute] int id, [FromBody] ProductsModel productsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productsModel.Product_id)
            {
                return BadRequest();
            }

            _context.Entry(productsModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsModelExists(id))
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

        // POST: api/ProductsModels
        [HttpPost]
        public async Task<IActionResult> PostProductsModel([FromBody] ProductsModel productsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProductsModel.Add(productsModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductsModel", new { id = productsModel.Product_id }, productsModel);
        }

        // DELETE: api/ProductsModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductsModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productsModel = await _context.ProductsModel.SingleOrDefaultAsync(m => m.Product_id == id);
            if (productsModel == null)
            {
                return NotFound();
            }

            _context.ProductsModel.Remove(productsModel);
            await _context.SaveChangesAsync();

            return Ok(productsModel);
        }

        private bool ProductsModelExists(int id)
        {
            return _context.ProductsModel.Any(e => e.Product_id == id);
        }
    }
}