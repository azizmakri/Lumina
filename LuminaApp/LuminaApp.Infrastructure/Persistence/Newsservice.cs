using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Infrastructure.Persistence
{
    public class NewsService : INewsService
    {
        private readonly IHubContext<NewsNotificationHub, INotificationHub> _newsNotification;
        private readonly LuminaAppContext _dbContext;
        private readonly IGenericRepository<SchoolNews> _newsRepo;
        private readonly IGenericRepository<NotificationEntity> _notificationRepo;
        private readonly IGenericRepository<User> _userRepo;
        private readonly IGenericRepository<DocumentEntity> _docRepo;
        private readonly UserManager<User> _userManager;

        public NewsService(
            IHubContext<NewsNotificationHub, INotificationHub> hubContext,
            UserManager<User> userManager,
            IGenericRepository<DocumentEntity> docRepo,
            IGenericRepository<NotificationEntity> notificationRepo,
            IGenericRepository<SchoolNews> newsRepo,
            LuminaAppContext context,
            IGenericRepository<User> userRepo)
        {
            _dbContext = context;
            _newsRepo = newsRepo;
            _userRepo = userRepo;
            _userManager = userManager;
            _docRepo = docRepo;
            _notificationRepo = notificationRepo;
            _newsNotification = hubContext;
        }

        public async Task AddNews(SchoolNews news, string adminId)
        {
            User admin = await _userRepo.GetByIdAsync(adminId);

            if (admin == null)
            {
                throw new Exception($"L'administrateur avec l'ID {adminId} n'a pas été trouvé.");
            }

            var userRoles = await _userManager.GetRolesAsync(admin);

            if (!userRoles.Contains("Admin"))
            {
                throw new UnauthorizedAccessException("L'utilisateur n'est pas autorisé à effectuer cette action.");
            }

            news.School = admin;

            await _newsRepo.CreateAsync(news);

            await _newsNotification.Clients.All.SendMessage(new NotificationEntity
            {
                Content = "nouveaux actualités par admin",
                Timestamp = DateTime.Now,
                newsFk = news.NewsId,
            });
            var notification = new NotificationEntity
            {
                Content = "nouveaux actualités par admin",
                Timestamp = DateTime.Now,
                newsFk = news.NewsId,
                schoolnews = news,
            };

            await _notificationRepo.CreateAsync(notification);            
        }
        public Task<IReadOnlyList<SchoolNews>> GetAllNews()
        {
            return  _newsRepo.GetAsync(); 
        }

        public async Task<byte[]> GetNewsByIdAsync(int NewsId)
        {
            var newsdocument = await _newsRepo.GetByIdAsync(NewsId);
            if (newsdocument == null)
            {
                throw new Exception($"La news avec l'ID {NewsId} n'a pas été trouvée.");
            }

            var filePath = newsdocument.NewsPath;
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Fichier non trouvé sur le disque", filePath);
            }

            return await System.IO.File.ReadAllBytesAsync(filePath);
        }
    }
}
