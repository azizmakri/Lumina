using AutoMapper;
using Lumina.Authentification.Application.Commons;
using Lumina.Authentification.Application.UtilisateurFeature.Commands.Create;
using Lumina.Authentification.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Application.UtilisateurFeature.Commands.ForgetPassword
{
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, OperationResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;


        public ForgetPasswordCommandHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<OperationResult> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var UserForget = _mapper.Map<User>(request);
            UserForget.Email = request.Email;

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                // L'utilisateur avec cet email n'existe pas
                return new OperationResult { Status = false, Message = "No user found with this email" };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // Vous pouvez maintenant envoyer ce jeton par e-mail à l'utilisateur


            return new OperationResult { Status = true, Message = "Password reset token generated successfully,le token est  ", Token = token };
        }
    }
}
