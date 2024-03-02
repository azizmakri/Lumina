using AutoMapper;
using Lumina.Authentification.Application.UtilisateurFeature;
using Lumina.Authentification.Application.UtilisateurFeature.Commands;
using Lumina.Authentification.Domain.Entities;
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
            CreateMap<User, UtilisateurDto>();
            CreateMap<CreateUtilisateurCommand, User>();
            CreateMap<UpdateUtilisateurCommand, User>();
        }
    }
}
