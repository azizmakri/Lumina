﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Application.UtilisateurFeature.Dtos
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
