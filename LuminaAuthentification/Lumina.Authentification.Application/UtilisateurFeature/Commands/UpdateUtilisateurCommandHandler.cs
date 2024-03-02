using AutoMapper;
using Lumina.Authentification.Application.Interfaces;
using Lumina.Authentification.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Application.UtilisateurFeature.Commands
{
    public class UpdateUtilisateurCommandHandler : IRequestHandler<UpdateUtilisateurCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IUtilisateurRepository _utilisateurRepository;

        public UpdateUtilisateurCommandHandler(IMapper mapper,IUtilisateurRepository utilisateurRepository)
        {
            this._mapper = mapper;
            this._utilisateurRepository = utilisateurRepository;
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
