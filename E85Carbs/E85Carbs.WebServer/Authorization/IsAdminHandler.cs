using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace E85Carbs.WebServer.Authorization
{
    public class IsAdminHandler : AuthorizationHandler<AllowedAdminAccess>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AllowedAdminAccess requirement)
        {
            var hasClaim = context.User.HasClaim(Claims.AdminOnly, "AdminOnly");

            if (hasClaim)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

    }
}
