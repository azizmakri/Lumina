using LuminaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.NotificationFeatures.Dtos
{
    public class NotificationDTO
    {
        public int NotificationId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public Boolean read { get; set; }
        public int newsFk { get; set; }
    }
}
