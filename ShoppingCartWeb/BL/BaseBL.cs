using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ShoppingCartWeb.BL
{
    public abstract class BaseBL
    {
        protected string BaseURL = "http://localhost:53827/api/";

        const string TokenKey = "_Token";
        private ISession Session;

        public BaseBL(IHttpContextAccessor httpContextAccessor)
        {
            Session = httpContextAccessor.HttpContext.Session;
        }

        private HttpClientHandler createClientHandler()
        {
			var handler = new HttpClientHandler();
			handler.ServerCertificateCustomValidationCallback = (msg, cert, chain, errors) => true;

			return handler;
        }

        protected HttpClient createClient(){

            HttpClient client = new HttpClient(createClientHandler());
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        protected async Task<HttpClient> createClientWithToken()
		{
			HttpClient client = new HttpClient(createClientHandler());
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", await GetToken());
			return client;
		}

        protected class TokenResponse
		{
			public class Data
			{
				[JsonProperty(PropertyName = "access_token")]
				public string AccessToken { get; set; }
				[JsonProperty(PropertyName = "expire_time")]
				public int ExpireTime { get; set; }
			}

			[JsonProperty(PropertyName = "data")]
			public Data DataResponse { get; set; }
			public string Error { get; set; }
		}


		public async Task<string> GetToken()
		{
			var currentToken = Session.GetString(TokenKey);
			if (currentToken != null && currentToken.Length > 0)
			{
				return currentToken;
			}
			else
			{
				HttpClient client = createClient();

                HttpResponseMessage response = await client.GetAsync(BaseURL + "users/generateToken");
				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadAsStringAsync();
					var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(json);
					var token = tokenResponse.DataResponse.AccessToken;

					Session.SetString(TokenKey, token);

					return token;
				}
				else
				{
					throw new Exception();
				}
			}
		}
    }
}
