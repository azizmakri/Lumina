using AutoMapper;
using Lumina.Authentification.Application.Commons;
using Lumina.Authentification.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;


namespace Lumina.Authentification.Application.UtilisateurFeature.Commands.login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult>
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public LoginCommandHandler(IConfiguration configuration, UserManager<User> userManager, IMapper mapper)
        {
            _configuration = configuration;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<OperationResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var userToLogin = _mapper.Map<User>(request);

            var user = await _userManager.FindByNameAsync(userToLogin.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, userToLogin.PasswordHash))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim("UserName", user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", user.Id),
                    new Claim("Email", user.Email),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName)
                };

                if (user.GradeFK.HasValue)
                {
                    authClaims.Add(new Claim("GradeFK", user.GradeFK.Value.ToString()));
                }

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim("Role", userRole));
                }

                var token = GetToken(authClaims);

                return new OperationResult
                {
                    Status = true,
                    Message = "Authentification réussie",
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };
            }

            return new OperationResult
            {
                Status = false,
                Message = "Échec de l'authentification"
            };
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }

}
