using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using Microsoft.AspNetCore.Cors;
using ShoppingCart.Repository;
using Microsoft.AspNetCore.Authorization;
using ShoppingCart.Filters;

namespace ShoppingCart.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Produces("application/json")]
    [Route("api/Products2")]
    [ValidateModel]
    public class Products2Controller : Controller
    {
        private readonly IProductRepository _productRepository;

        public Products2Controller(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/Products
        [Authorize("Bearer")]
        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts([FromHeader] string Authorization)
        {
            return await _productRepository.GetAll();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        [ValidateProductExists]
        public async Task<IActionResult> GetProducts([FromRoute] int id)
        {
            return Ok(await _productRepository.Get(id));
     
        }

        // PUT: api/Products2/5
        [HttpPut("{id}")]
        [ValidateProductExists]
        public async Task<IActionResult> PutProducts([FromRoute] int id, [FromBody] Product product)
        {
            await _productRepository.Update(product);
            return Ok();


        }

        // POST: api/Products2
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Product product)
        {
            await _productRepository.Save(product);
            return Ok(product);

        }

        // DELETE: api/Products2/5
        [HttpDelete("{id}")]
        [ValidateProductExists]
        public async Task<IActionResult> DeleteProducts([FromRoute] int id)
        {
            await _productRepository.Delete(id);
            return Ok();
        }
    }
}