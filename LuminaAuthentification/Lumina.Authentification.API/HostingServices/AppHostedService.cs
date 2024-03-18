
using Lumina.Authentification.Application.UtilisateurFeature.Commands.CreateAdmin;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Lumina.Authentification.API.HostingServices
{
    public class AppHostedService : IHostedService
    {
        private readonly IMediator _mediator;

        public AppHostedService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            /*CreateAdminCommand user=new CreateAdminCommand{ 
                FirstName="Admin",
                LastName="Admin",
                Email = "admin@admin.com",
                PasswordHash = "Azerty-20"
            };
            var res=_mediator.Send(user);*/
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
