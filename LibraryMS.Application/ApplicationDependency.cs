using AutoMapper;
using LibraryMS.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryMS.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register application services here
            // e.g., services.AddTransient<IBookService, BookService>();
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
            return services;
        }
    }
}
