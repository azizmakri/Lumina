using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LuminaApp.Domain.Entities;
using System.Reflection;
using LuminaApp.Application.Interfaces;
using LuminaApp.Infrastructure.Persistence;
using LuminaApp.Application.MappingProfiles;
using Microsoft.AspNetCore.SignalR;

namespace LuminaApp.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer<LuminaAppContext>(configuration.GetConnectionString("LuminaContextConnection"), null);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddDbContext<LuminaAppContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("LuminaContextConnection"), b => b.MigrationsAssembly("Presentation")));

            // Mappers registrations
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(SessionProfile));
            services.AddAutoMapper(typeof(SubjectProfile));
            services.AddAutoMapper(typeof(EvaluationProfile));
            services.AddAutoMapper(typeof(AttendanceProfile));
            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(GradeProfile));
            services.AddAutoMapper(typeof(NotificationProfile));
            services.AddAutoMapper(typeof(ClassRoomProfile));

            // Services registrations
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IEvaluationService, EvaluationService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IClassRoomService, ClassRoomService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGradeService, GradeService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IClassRoomService, ClassRoomService>();


            // Repository registrations
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            services.AddScoped<IGenericRepository<Subject>, GenericRepository<Subject>>();
            services.AddScoped<IGenericRepository<Session>, GenericRepository<Session>>();
            services.AddScoped<IGenericRepository<Attendance>, GenericRepository<Attendance>>();
            services.AddScoped<IGenericRepository<ClassRoom>, GenericRepository<ClassRoom>>();
            services.AddScoped<IGenericRepository<DocumentEntity>, GenericRepository<DocumentEntity>>();
            services.AddScoped<IGenericRepository<Equipment>, GenericRepository<Equipment>>();
            services.AddScoped<IGenericRepository<Evaluation>, GenericRepository<Evaluation>>();
            services.AddScoped<IGenericRepository<Folder>, GenericRepository<Folder>>();
            services.AddScoped<IGenericRepository<History>, GenericRepository<History>>();
            services.AddScoped<IGenericRepository<Grade>, GenericRepository<Grade>>();
            services.AddScoped<IGenericRepository<SchoolNews>, GenericRepository<SchoolNews>>();
            services.AddScoped<IGenericRepository<NotificationEntity>, GenericRepository<NotificationEntity>>();
            services.AddScoped<IGenericRepository<ClassRoom>, GenericRepository<ClassRoom>>();

            services.AddScoped<UserManager<User>>();
            services.AddScoped<SignInManager<User>>();

            // Add SignalR
            services.AddSignalR();

            return services;
        }
    }
}
