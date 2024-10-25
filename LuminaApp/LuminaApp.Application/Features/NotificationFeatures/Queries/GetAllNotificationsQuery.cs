using LuminaApp.Application.Features.NotificationFeatures.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.NotificationFeatures.Queries
{
    public class GetAllNotificationsQuery:IRequest<IReadOnlyList<NotificationDTO>>
    {
    }
}
