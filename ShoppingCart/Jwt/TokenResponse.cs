using System;
using Newtonsoft.Json;

namespace ShoppingCart.Jwt
{
    public class TokenResponse
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
}
