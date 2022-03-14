using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace foodies_app.Controllers
{
    public class WebSocketController
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
              .UseStartup<Program>();
    }
}
