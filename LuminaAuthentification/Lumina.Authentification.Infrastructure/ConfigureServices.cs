using Lumina.Authentification.Application.Interfaces;
using Lumina.Authentification.Application.UtilisateurFeature.Commands;
using Lumina.Authentification.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            //optionsBuilder => optionsBuilder.EnableSensitiveDataLogging(builder.Environment.IsDevelopment()));
            //Add Database service

            services.AddDbContext<LuminaContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("LuminaContextConnection"), b => b.MigrationsAssembly("Presentation")));


            return services;
        }
    }
}
