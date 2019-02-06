using Microsoft.AspNetCore.Builder;

namespace MailService.API.Infrastructure
{
    /// <summary>
    /// Extension methods for <see cref="IApplicationBuilder"/>
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds global exception handler
        /// </summary>
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
