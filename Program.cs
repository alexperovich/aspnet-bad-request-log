using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace aspnetrepro
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = new WebHostBuilder();
            hostBuilder.UseHttpSys();
            hostBuilder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddFilter(level => true);
                logging.AddConsole();
            });
            hostBuilder.UseStartup<Startup>();

            var host = hostBuilder.Build();
            host.Run();
        }
    }

    public class Startup : IStartup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Use((HttpContext ctx, Func<Task> next) =>
            {
                throw new Exception();
            });
        }
    }
}