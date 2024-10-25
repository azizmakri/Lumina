using AutoMapper;
using LuminaApp.Application.Commons;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.NotificationFeatures.Commands
{
    public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, OperationResult>
    {
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IGenericRepository<NotificationEntity> _notificationRepo;

        public UpdateNotificationCommandHandler(IGenericRepository<NotificationEntity> notificationRepo, IMapper mapper,INotificationService notificationService)
        {
            _mapper = mapper;
            _notificationService = notificationService;
            _notificationRepo = notificationRepo;
        }
        public async Task<OperationResult> Handle( UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var notificationToUpdate = await _notificationRepo.GetByIdAsync(request.notificationId);
                if (notificationToUpdate == null)
                {
                    return new OperationResult { Status = false, Message = "Notification non trouvée." };
                }

                // Update only the start and end hour properties
                notificationToUpdate.read = true;

                await _notificationService.ReadNotification(notificationToUpdate);

                return new OperationResult { Status = true, Message = "La notification a été mise à jour avec succès." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Status = false, Message = $"Échec de la mise à jour de la notification : {ex.Message}" };
            }
        }
    }
}
