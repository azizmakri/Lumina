using AutoMapper;
using Lumina.Authentification.Application.Interfaces;
using Lumina.Authentification.Application.UtilisateurFeature.Dtos;
using MediatR;

namespace Lumina.Authentification.Application.UtilisateurFeature.Queries.AllUsers
{
    public class GetUtilisateursQueryHandler : IRequestHandler<GetUtilisateursQuery, List<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUtilisateurRepository _utilisateurRepository;

        public GetUtilisateursQueryHandler(IMapper mapper, IUtilisateurRepository utilisateurRepository)
        {
            _mapper = mapper;
            _utilisateurRepository = utilisateurRepository;
        }
        public async Task<List<UserDto>> Handle(GetUtilisateursQuery request, CancellationToken cancellationToken)
        {
            //Query the Database
            var utilisateurs = await _utilisateurRepository.GetAsync();
            // convert data objects into DTO objects
            var data = _mapper.Map<List<UserDto>>(utilisateurs);
            //return the list DTO object

            return data;
        }
    }
}
