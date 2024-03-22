using AutoMapper;
using Lumina.Authentification.Application.Commons;
using Lumina.Authentification.Application.Commons.Email;
using Lumina.Authentification.Application.Interfaces;
using Lumina.Authentification.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Lumina.Authentification.Application.UtilisateurFeature.Commands.ForgetPassword
{
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, OperationResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;

        public ForgetPasswordCommandHandler(UserManager<User> userManager, IMapper mapper,IMailService mailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _mailService = mailService;
        }

        public async Task<OperationResult> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            //var UserForget = _mapper.Map<User>(request);
            //UserForget.Email = request.Email;

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                // L'utilisateur avec cet email n'existe pas
                return new OperationResult { Status = false, Message = "No user found with this email" };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // Vous pouvez maintenant envoyer ce jeton par e-mail à l'utilisateur
            // Construct a more expressive email body
            var resetLink = $"http://localhost:4200/changepwd?userId={user.Id}&token={WebUtility.UrlEncode(token)}";
            var body = $"Hello {user.UserName},\n\nYou have requested to reset your password. Click the link below to reset your password:\n\n{resetLink}\n\nIf you did not request this reset, please ignore this email.";

            var data = new MailData(user.Id, user.Email, "JobyHunter Reset Password", body);
            _mailService.SendMail(data);

            return new OperationResult { Status = true, Message = "Password reset token generated successfully,le token est  ", Token = token };
        }
    }
}
