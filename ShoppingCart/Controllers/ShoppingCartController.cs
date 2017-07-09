using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Repository;
using ShoppingCart.Models;
using ShoppingCart.Controllers.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace ShoppingCart.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Produces("application/json")]
    [Route("api/cart")]
    public class ShoppingCartController : Controller
    {
        private IShoppingCartRepository Repository;

        public ShoppingCartController(IShoppingCartRepository repository)
        {
            Repository = repository;
        }

		// POST: api/cart/add
		[Authorize("Bearer")]
        [HttpPost("add")]
        public async Task<SimpleResponse> AddToCart([FromHeader] string Authorization,
                                                    [FromBody] CartRequest request)
        {
            var success = await Repository.AddToCart(request.productId, request.deviceId, request.userId);
            return new SimpleResponse() { Success = success };
        }


        [Authorize("Bearer")]
        [HttpPost("remove")]
        public async Task<SimpleResponse> RemoveFromCart([FromHeader] string Authorization,
                                                         [FromBody] CartRequest request)
        {
            var success = await Repository.RemoveFromCart(request.productId, request.deviceId, request.userId);
            return new SimpleResponse() { Success = success };
        }

        [Authorize("Bearer")]
        [HttpGet("current")]
        public async Task<ShoppingCartResponse> GetCurrentCartFor([FromHeader] string Authorization,
                                                                  [FromQuery] string deviceId,
                                                                 [FromQuery] string userId)
        {
            var cart = await Repository.GetCurrentCartForUser(deviceId, null);
            var response = new ShoppingCartResponse();
            if (cart != null)
            {
                var data = new ShoppingCartResponse.CartData();
                data.Id = cart.ShoppingCartId.ToString();
                data.Products = cart.Products ?? new List<Product>();
                response.DataResponse = data;
            }
            return response;
        }



    }
}
