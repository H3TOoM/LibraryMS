using LibraryMS.Domain.Repoistries;
using LibraryMS.Infrastructure.Data;
using LibraryMS.Infrastructure.Repoistries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryMS.Infrastructure
{
    /// <summary>
    /// Static class for configuring infrastructure layer dependencies
    /// Registers database context, repositories, and other infrastructure services
    /// </summary>
    public static class InfrastructureDependencyInjection
    {
        /// <summary>
        /// Adds infrastructure layer services and configurations to the dependency injection container
        /// </summary>
        /// <param name="services">The service collection to add services to</param>
        /// <param name="configuration">Application configuration for database connection strings</param>
        /// <returns>The updated service collection</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Database Configuration

            // Configure Entity Framework Core with SQL Server
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            #endregion

            // TODO: Register repository implementations and other infrastructure services here
            services.AddScoped(typeof(IMainRepoistery<>), typeof(MainRepoistery<>));
            services.AddScoped<IBookRepoistory,BookRepoistory>()
                .AddScoped<IBorrowRepoistory,BorrowRepoistory>()
                .AddScoped<IMemberRepoistory,MemberRepoistory>()
                .AddScoped<IAccountRepoistory,AccountRepoistory>()
                .AddScoped<IUnitOfWork,UnitOfWork>();


            return services;
        }
    }
}
