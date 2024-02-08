using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace E85Carbs.WebServer.Authorization
{
    public class IsUserHandler : AuthorizationHandler<AllowedUserAccess>
    {
        
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AllowedUserAccess requirement)
        {
            var hasClaim = context.User.HasClaim(Claims.IsUser, "IsUser");

            if (hasClaim)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        
    }
}
