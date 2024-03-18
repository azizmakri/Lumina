using AutoMapper;
using Lumina.Authentification.Application.UtilisateurFeature.Commands.Create;
using Lumina.Authentification.Application.UtilisateurFeature.Commands.CreateAdmin;
using Lumina.Authentification.Application.UtilisateurFeature.Commands.CreateEleve;
using Lumina.Authentification.Application.UtilisateurFeature.Commands.ForgetPassword;
using Lumina.Authentification.Application.UtilisateurFeature.Commands.login;
using Lumina.Authentification.Application.UtilisateurFeature.Commands.ResetPassword;
using Lumina.Authentification.Application.UtilisateurFeature.Commands.Update;
using Lumina.Authentification.Application.UtilisateurFeature.Dtos;
using Lumina.Authentification.Domain.Entities;
using Lumina.Authentification.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Application.MappingProfiles
{
    public class UtilisateurProfile : Profile
    {
        public UtilisateurProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<LoginCommand, User>();
            CreateMap<ForgetPasswordCommand, User>();
            CreateMap<ResetPasswordCommand, User>();
            CreateMap<CreateUtilisateurCommand, User>().ReverseMap()
                .ForMember(x => x.Role, opt => opt.Ignore());
            CreateMap<UpdateUtilisateurCommand, User>();
            CreateMap<CreateAdminCommand, User>();
            CreateMap<CreateEleveCommand, User>();

        }
    }
}
