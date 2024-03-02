using AutoMapper;
using Lumina.Authentification.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Application.UtilisateurFeature.Queries
{
    public class GetUtilisateursQueryHandler : IRequestHandler<GetUtilisateursQuery, List<UtilisateurDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUtilisateurRepository _utilisateurRepository;

        public GetUtilisateursQueryHandler(IMapper mapper, IUtilisateurRepository utilisateurRepository)
        {
            _mapper = mapper;
            _utilisateurRepository = utilisateurRepository;
        }
        public async Task<List<UtilisateurDto>> Handle(GetUtilisateursQuery request, CancellationToken cancellationToken)
        {
            //Query the Database
            var utilisateurs = await _utilisateurRepository.GetAsync();
            // convert data objects into DTO objects
            var data = _mapper.Map<List<UtilisateurDto>>(utilisateurs);
            //return the list DTO object

            return data;
        }
    }
}
