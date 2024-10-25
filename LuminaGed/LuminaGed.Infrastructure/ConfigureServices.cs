using LuminaApp.Infrastructure.Persistence;
using LuminaGed.Application.Interfaces;
using LuminaGed.Application.MappingProfiles;
using LuminaGed.Domain.Entities;
using LuminaGed.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer<LuminaGedContext>(
                configuration.GetConnectionString("LuminaContextConnection"), null);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddDbContext<LuminaGedContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("LuminaContextConnection"), b => b.MigrationsAssembly("Presentation")));

            //Mappers registrations
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(FolderProfile));
            services.AddAutoMapper(typeof(DocProfile));



            //Services registrations
            services.AddScoped<IFolderService, FolderService>();
            services.AddScoped<IDocService, DocumentService>();


            //Repository registrations
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
     
            services.AddScoped<IGenericRepository<DocumentEntity>, GenericRepository<DocumentEntity>>();
        
            services.AddScoped<IGenericRepository<Folder>, GenericRepository<Folder>>();
            services.AddScoped<IGenericRepository<Grade>, GenericRepository<Grade>>();



            services.AddScoped<UserManager<User>>();
            services.AddScoped<SignInManager<User>>();

            return services;
        }
    }
}
