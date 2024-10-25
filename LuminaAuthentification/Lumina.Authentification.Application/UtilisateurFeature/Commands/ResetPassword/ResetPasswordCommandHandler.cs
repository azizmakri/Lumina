using AutoMapper;
using Lumina.Authentification.Application.Commons;
using Lumina.Authentification.Application.UtilisateurFeature.Commands.login;
using Lumina.Authentification.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Application.UtilisateurFeature.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, OperationResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ResetPasswordCommandHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<OperationResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return new OperationResult { Status = false, Message = "Aucun utilisateur n'a été trouvé avec cet email" };
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

            if (result.Succeeded)
            {
                return new OperationResult { Status = true, Message = "Réinitialisation du mot de passe réussie" };
            }
            else
            {
                return new OperationResult { Status = false, Message = "Échec de la réinitialisation du mot de passe" };
            }
        }
    }
}
