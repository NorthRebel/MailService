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

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDbContext(services);
            BindServices(services);
            BindRepositories(services);
            ConfigureCORS(services);
            services.AddMvc();
        }

        private void ConfigureDbContext(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MailServiceDbContext>(options => options.UseSqlServer(connectionString));
        }

        private void BindServices(IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddSingleton<IEmailSender>(Configuration.GetSection("SenderCredentials").Get<EmailSender>());
        }

        private void BindRepositories(IServiceCollection services)
        {
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IRecipientRepository, RecipientRepository>();
            services.AddScoped<ICorrespondenceRepository, CorrespondenceRepository>();
        }

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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/mails");
            });
        }
    }
}
