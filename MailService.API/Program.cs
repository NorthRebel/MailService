using Microsoft.AspNetCore;
using MailService.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MailService.API
{
    public class Program
    {
        /// <summary>
        /// Entry point of application.
        /// </summary>
        /// <param name="args">Additional args (not used)</param>
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    // Migrates database if not exits.
                    var context = scope.ServiceProvider.GetService<MailServiceDbContext>();
                    context.Database.Migrate();
                }
                catch
                {
                    // Use logger
                }
            }

            host.Run();
        }

        /// <summary>
        /// Configures host to run web application
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
