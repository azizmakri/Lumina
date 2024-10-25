using AutoMapper;
using Lumina.Authentification.Application.Interfaces;
using Lumina.Authentification.Domain.Entities;
using MediatR;

namespace Lumina.Authentification.Application.UtilisateurFeature.Commands.Update
{
    public class UpdateUtilisateurCommandHandler : IRequestHandler<UpdateUtilisateurCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IUtilisateurRepository _utilisateurRepository;

        public UpdateUtilisateurCommandHandler(IMapper mapper, IUtilisateurRepository utilisateurRepository)
        {
            _mapper = mapper;
            _utilisateurRepository = utilisateurRepository;
        }
        public async Task<Unit> Handle(UpdateUtilisateurCommand request, CancellationToken cancellationToken)
        {
            //validate incoming data

            //convert to domain entity object
            var utilisateurToUpdate = _mapper.Map<User>(request);
            //add to database
            await _utilisateurRepository.UpdateAsync(utilisateurToUpdate);
            //return
            return Unit.Value;
        }
    }
}
