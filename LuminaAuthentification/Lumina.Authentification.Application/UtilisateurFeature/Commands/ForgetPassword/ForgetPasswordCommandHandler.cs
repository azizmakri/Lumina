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
                return new OperationResult { Status = false, Message = "Aucun utilisateur n'a été trouvé avec cet email" };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // Vous pouvez maintenant envoyer ce jeton par e-mail à l'utilisateur
            // Construct a more expressive email body
            var resetLink = $"http://localhost:4200/changepwd?userId={user.Id}&token={WebUtility.UrlEncode(token)}";
            var body = $"Bonjour {user.UserName},\n\nVous avez demandé à réinitialiser votre mot de passe. Cliquez sur le lien ci-dessous pour réinitialiser votre mot de passe:\n\n{resetLink}\n\nSi vous n'avez pas demandé cette réinitialisation, veuillez ignorer cet e-mail.";

            var data = new MailData(user.Id, user.Email, "Réinitialisation du mot de passe de Lumina", body);
            _mailService.SendMail(data);

            return new OperationResult { Status = true, Message = "Lien de réinitialisation du mot de passe générée avec succès consultez votre courrier" };
        }
    }
}
