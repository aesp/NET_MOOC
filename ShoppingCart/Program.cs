using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace ShoppingCart
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cert = new X509Certificate2("localhost.pfx", "123456");
            try{

				var host = new WebHostBuilder()

					.UseKestrel(options =>
					{
						options.UseHttps(cert);
						options.UseConnectionLogging();
						options.NoDelay = true;
					})
					//.UseUrls("https://*:4439")
					.UseContentRoot(Directory.GetCurrentDirectory())
					.UseIISIntegration()
					.UseStartup<Startup>()
					.UseApplicationInsights()
					.Build();

				host.Run();
            }catch(Exception e){
                Console.WriteLine("Exception: " + e.ToString());
            }
        }
    }
}
