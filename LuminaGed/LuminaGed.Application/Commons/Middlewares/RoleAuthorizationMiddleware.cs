using LuminaGed.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace LuminaGed.Application.Commons.Middlewares
{
    public class RoleAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public RoleAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                using (var scope = context.RequestServices.CreateScope())
                {
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    if (userId != null)
                    {
                        var user = await userManager.FindByIdAsync(userId);
                        if (user != null)
                        {
                            var roles = await userManager.GetRolesAsync(user);
                            context.Items["UserRoles"] = roles;
                        }
                    }
                }
            }

            await _next(context);
        }
    }
}
