using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using ShoppingCart.Repository;

namespace ShoppingCart.Controllers
{
    [Produces("application/json")]
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private IProductRepository Repository;

        public ProductsController(IProductRepository repository)
        {
            Repository = repository;
        }

        // GET: api/Products
        [Authorize("Bearer")]
        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts([FromHeader] string Authorization)
        {
            return await Repository.GetAll();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var products = await Repository.Get(id);

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts([FromRoute] int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            if(await Repository.Update(product)){
                return NoContent();
            }else{
                return NotFound();
            }

        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(await Repository.Save(product)){
                return CreatedAtAction("GetProducts", new { id = product.ProductId }, product);
            }else{
                return NotFound();
            }



        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var success = Repository.Delete(id);

            if (await Repository.Delete(id))
            {
                return Ok("{ \"msg\" : \"Success\" }");
            }
            else
            {
                return NoContent();
            }
        }

		// GET: api/products/search
		[Authorize("Bearer")]
        [HttpGet("search")]
        public async Task<IEnumerable<Product>> Search([FromHeader] string Authorization, [FromQuery] string query)
		{
            return await Repository.Search(query);
		}


	}
}