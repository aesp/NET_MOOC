using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoppingCartWeb.Models.ProductViewModels;

namespace ShoppingCartWeb.BL
{
    public class ProductBL : BaseBL
    {


        public ProductBL(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor){}

        public async Task<IEnumerable<ProductViewModel>> GetProduct()
        {
            var client = await createClientWithToken();

			HttpResponseMessage response = await client.GetAsync(BaseURL + "products");
			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<IEnumerable<ProductViewModel>>(json);
                return products;
            }else
            {
                return new List<ProductViewModel>();
            }
        }

    }
}
