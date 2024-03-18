using Lumina.Authentification.Application.Interfaces;
using MediatR;

namespace Lumina.Authentification.Application.UtilisateurFeature.Commands.Delete
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
            var utilisateurToDelete = await _utilisateurRepository.GetByIdAsync(request.Id);
            //add to database
            await _utilisateurRepository.DeleteAsync(utilisateurToDelete);
            //return record id
            return Unit.Value;
        }
    }
}
