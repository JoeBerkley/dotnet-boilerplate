using Data.Contexts;
using Identity.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace System
{
    public static class DatabaseExtensions
    {
        public static async Task MigrateDatabaseAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            try
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();
                logger.LogError(ex, "An error occurred creating the Application DB.");
            }

            try
            {
                var identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
                await identityContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<IdentityContext>>();
                logger.LogError(ex, "An error occurred creating the IdentityId DB.");
            }
        }
    }
}