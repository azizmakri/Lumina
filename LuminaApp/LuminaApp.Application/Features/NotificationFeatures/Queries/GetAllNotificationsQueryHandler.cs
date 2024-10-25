using AutoMapper;
using LuminaApp.Application.Features.ClassRoomFeatures.Dtos;
using LuminaApp.Application.Features.ClassRoomFeatures.Queries.GetAllClasses;
using LuminaApp.Application.Features.NotificationFeatures.Dtos;
using LuminaApp.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.NotificationFeatures.Queries
{
    public class GetAllNotificationsQueryHandler : IRequestHandler<GetAllNotificationsQuery, IReadOnlyList<NotificationDTO>>
    {
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public GetAllNotificationsQueryHandler(IMapper mapper, INotificationService notificationService)
        {
            _mapper = mapper;
            _notificationService = notificationService;
        }
        public async Task<IReadOnlyList<NotificationDTO>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notifications = await _notificationService.GetAllNotifications();
            var classDTO = _mapper.Map<IReadOnlyList<NotificationDTO>>(notifications);
            return classDTO;
        }
    }
}
