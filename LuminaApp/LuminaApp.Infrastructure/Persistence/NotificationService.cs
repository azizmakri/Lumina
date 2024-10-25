using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Infrastructure.Persistence
{
    public class NotificationService : INotificationService
    {

        private readonly IGenericRepository<NotificationEntity> _notificationRepo;

        public NotificationService(IGenericRepository<NotificationEntity> notificationRepo)
        {
            _notificationRepo = notificationRepo;
        }
        public async Task<IReadOnlyList<NotificationEntity>> GetAllNotifications()
        {
            IReadOnlyList<NotificationEntity> classrooms = await _notificationRepo.GetAsync();
            return classrooms;
        }

        public async Task ReadNotification(NotificationEntity notification)
        {
            try
            {
                await _notificationRepo.UpdateAsync(notification);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Une erreur s'est produite lors de la mise à jour de la notification.", ex);
            }
        }
    }
}
