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
    public class DeleteUtilisateurCommandHandler : IRequestHandler<DeleteUtilisateurCommand, Unit>
    {

        private readonly IUtilisateurRepository _utilisateurRepository;

        public DeleteUtilisateurCommandHandler(IUtilisateurRepository utilisateurRepository)
        {
            _utilisateurRepository = utilisateurRepository;
        }
        public async Task<Unit> Handle(DeleteUtilisateurCommand request, CancellationToken cancellationToken)
        {
            //retrieve domain entity object
            var utilisateurToDelete = await _utilisateurRepository.GetByIdAsync(request.Identifiant);
            //add to database
            await _utilisateurRepository.DeleteAsync(utilisateurToDelete);
            //return record id
            return Unit.Value;
        }
    }
}
