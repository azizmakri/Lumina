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
    public class CreateUtilisateurCommandHandler : IRequestHandler<CreateUtilisateurCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IUtilisateurRepository _utilisateurRepository;

        public CreateUtilisateurCommandHandler(IMapper mapper, IUtilisateurRepository utilisateurRepository)
        {
            _mapper = mapper;
            _utilisateurRepository = utilisateurRepository;
        }
        public async Task<int> Handle(CreateUtilisateurCommand request, CancellationToken cancellationToken)
        {
            //validate incoming data


            //convert to domain entity object
            var utilisateurToCreate = _mapper.Map<User>(request);
            //add to database
            await _utilisateurRepository.CreateAsync(utilisateurToCreate);
            //return record id
            return utilisateurToCreate.UserId;
        }
    }
}
