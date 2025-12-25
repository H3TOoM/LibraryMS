using AutoMapper;
using LibraryMS.Application.Helpers;
using LibraryMS.Application.Interfaces;
using LibraryMS.Application.Mapping;
using LibraryMS.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibraryMS.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration Configuration)
        {
            // Register application services here
            // e.g., services.AddTransient<IBookService, BookService>();

            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            services.AddScoped<IAccountService, AccountService>()
                .AddScoped<IBookService, BookService>()
                .AddScoped<IFineService, FineService>()
                .AddScoped<IMemberService, MemberService>()
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IBorrowService, BorrowService>();


            // Configure JWT settings
            var jwtSettingsSection = Configuration.GetSection("JwtSettings");
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


            return services;
        }
    }


}
