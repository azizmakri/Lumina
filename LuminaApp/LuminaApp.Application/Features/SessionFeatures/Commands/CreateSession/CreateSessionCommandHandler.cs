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

namespace LuminaApp.Application.Features.SessionFeatures.Commands.CreateSession
{
    public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, OperationResult>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public CreateSessionCommandHandler(IMapper mapper, ISessionService sessionRepository)
        {
            _mapper = mapper;
            _sessionService = sessionRepository;
        }


        public async Task<OperationResult> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            var sessionToCreate = _mapper.Map<Session>(request);

            try
            {
                await _sessionService.AddSession(sessionToCreate, request.subjectFK,request.classRoomFK) ;
                return new OperationResult { Status = true, Message = "Séance créée avec succès" };
            }
            catch (Exception ex)
            {
                return new OperationResult { Status = false, Message = $"Échec de la création de la séance: {ex.Message}" };
            }
        }
    }
}
