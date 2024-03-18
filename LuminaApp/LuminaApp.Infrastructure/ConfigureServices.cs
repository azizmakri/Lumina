using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LuminaApp.Domain.Entities;
using System.Reflection;

namespace LuminaApp.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer<LuminaAppContext>(
                configuration.GetConnectionString("LuminaContextConnection"), null);

                       services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
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

            services.AddDbContext<LuminaAppContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("LuminaContextConnection"), b => b.MigrationsAssembly("Presentation")));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<UserManager<User>>();
            services.AddScoped<SignInManager<User>>();

            return services;
        }
    }
}
