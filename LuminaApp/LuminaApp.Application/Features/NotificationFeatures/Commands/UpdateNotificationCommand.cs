using LuminaApp.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.NotificationFeatures.Commands
{
    public class UpdateNotificationCommand:IRequest<OperationResult>
    {
        public int notificationId { get; set; }
    }
}
