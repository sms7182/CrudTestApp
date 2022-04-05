
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mc2.CrudAppTest.Api.Data.Configuration
{
    public static class PostgresConfiguration
    {
        public static IServiceCollection AddPostgres<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
        {

            services.AddEntityFrameworkNpgsql().AddDbContext<T>(options =>
            {
                options.UseNpgsql(configuration["ConnectionStrings:defaultConnection"], d => d.MigrationsAssembly(typeof(T).Assembly.GetName().Name));
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            });

            return services;

        }
    }
}
