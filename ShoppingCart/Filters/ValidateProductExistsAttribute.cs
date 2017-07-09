using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShoppingCart.Repository;

namespace ShoppingCart.Filters
{   
    public class ValidateProductExistsAttribute : TypeFilterAttribute
    {
        public ValidateProductExistsAttribute():base(typeof(ValidateProductExistsAttributeImpl))
        {
        }

        private class ValidateProductExistsAttributeImpl : IAsyncActionFilter
        {
            private readonly IProductRepository _productRepository;

            public ValidateProductExistsAttributeImpl(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("id"))
                {
                    var id = context.ActionArguments["id"] as int?;
                    if (id.HasValue)
                    {
                        if ((await _productRepository.GetAll()).All(a => a.ProductId != id.Value))
                        {
                            context.Result = new NotFoundObjectResult(id.Value);
                            return;
                        }
                    }
                }
                await next();
            }
        }
    }
}
