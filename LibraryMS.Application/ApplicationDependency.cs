using AutoMapper;
using LibraryMS.Application.Helpers;
using LibraryMS.Application.Mapping;
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
       
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            #region Service Registrations

            // Register AutoMapper with custom mapping profiles
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
            services.AddSingleton<JwtSecurityTokenHandler>();

            // Services are no longer used - replaced with MediatR features
            // All business logic is now handled through MediatR commands and queries

            #endregion


            // MediatR Configuration
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationDependencyInjection).Assembly));



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
