using LuminaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Interfaces
{
    public interface INotificationService
    {
        public Task<IReadOnlyList<NotificationEntity>> GetAllNotifications();
        public Task ReadNotification(NotificationEntity notification);
    }
}
