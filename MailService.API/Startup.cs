using MailService.Services.Mail;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MailService.API
{
    using MailService.Persistence;
    using MailService.API.Infrastructure;
    using MailService.Domain.Repositories;
    using MailService.Infrastructure.Mail;
    using MailService.Persistence.Repositories;

    /// <summary>
    /// Configures web application 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration of application that stored in appsettings.json
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Constructor that inject configuration of application that stored in appsettings.json
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configures services of web app
        /// </summary>
        /// <param name="services">Native IoC-Container</param>
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDbContext(services);
            BindServices(services);
            BindRepositories(services);
            ConfigureCORS(services);
            services.AddMvc();
        }

        /// <summary>
        /// Configures EF Core database context
        /// </summary>
        private void ConfigureDbContext(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MailServiceDbContext>(options => options.UseSqlServer(connectionString));
        }

        /// <summary>
        /// Injects implementation of abstract services
        /// </summary>
        private void BindServices(IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddSingleton<IEmailSender>(Configuration.GetSection("SenderCredentials").Get<EmailSender>());
        }

        /// <summary>
        /// Injects implementation of abstract repositories
        /// </summary>
        private void BindRepositories(IServiceCollection services)
        {
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IRecipientRepository, RecipientRepository>();
            services.AddScoped<ICorrespondenceRepository, CorrespondenceRepository>();
        }

        /// <summary>
        /// Configures CORS policy to allow access for all clients
        /// </summary>
        private void ConfigureCORS(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("AllowAll",
                builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                }));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.ConfigureCustomExceptionMiddleware();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/mails");
            });
        }
    }
}
