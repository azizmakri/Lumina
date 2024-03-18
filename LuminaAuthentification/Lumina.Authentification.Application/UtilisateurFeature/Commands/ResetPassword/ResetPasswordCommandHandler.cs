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
                return new OperationResult { Status = false, Message = "No user found with this email" };
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

            if (result.Succeeded)
            {
                return new OperationResult { Status = true, Message = "Password reset successfully" };
            }
            else
            {
                return new OperationResult { Status = false, Message = "Failed to reset password" };
            }
        }
    }
}
