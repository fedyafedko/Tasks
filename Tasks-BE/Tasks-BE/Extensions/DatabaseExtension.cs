using Microsoft.EntityFrameworkCore;
using Tasks.DAL.EF;

namespace Tasks_BE.Extensions
{
    public static class DatabaseExtension
    {
        public static void MigrateDatabase(this IHost app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
        }
    }
}
