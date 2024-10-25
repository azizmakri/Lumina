using Lumina.Authentification.Application.Interfaces;
using Lumina.Authentification.Application.MappingProfiles;
using Lumina.Authentification.Domain.Entities;
using Lumina.Authentification.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Lumina.Authentification.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer<LuminaContext>(
                configuration.GetConnectionString("LuminaContextConnection"), null);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();
            services.AddTransient<IMailService, MailService>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });

            services.AddAutoMapper(typeof(UtilisateurProfile));
            services.AddDbContext<LuminaContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("LuminaContextConnection"), b => b.MigrationsAssembly("Presentation")));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<UserManager<User>>();
            services.AddScoped<SignInManager<User>>();

            return services;
        }
    }
}
