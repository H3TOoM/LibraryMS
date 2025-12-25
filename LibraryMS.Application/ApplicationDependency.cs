using AutoMapper;
using LibraryMS.Application.Helpers;
using LibraryMS.Application.Interfaces;
using LibraryMS.Application.Mapping;
using LibraryMS.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LibraryMS.Application
{
    /// <summary>
    /// Static class for configuring application layer dependencies
    /// Registers services, AutoMapper profiles, and JWT authentication
    /// </summary>
    public static class ApplicationDependencyInjection
    {
        /// <summary>
        /// Adds application layer services and configurations to the dependency injection container
        /// </summary>
        /// <param name="services">The service collection to add services to</param>
        /// <param name="configuration">Application configuration for JWT settings</param>
        /// <returns>The updated service collection</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            #region Service Registrations

            // Register AutoMapper with custom mapping profiles
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
            services.AddSingleton<JwtSecurityTokenHandler>();

            // Register all application services with scoped lifetime
            services.AddScoped<IAccountService, AccountService>()
                .AddScoped<IBookService, BookService>()
                .AddScoped<IFineService, FineService>()
                .AddScoped<IMemberService, MemberService>()
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IBorrowService, BorrowService>();

            #endregion

            #region JWT Authentication Configuration
            var jwtSettingsSection = configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);
            var jwtSettings = jwtSettingsSection.Get<JwtSettings>() ?? new JwtSettings();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey))
                };
            });

            services.AddAuthorization();

            #endregion

            return services;
        }
    }


}
