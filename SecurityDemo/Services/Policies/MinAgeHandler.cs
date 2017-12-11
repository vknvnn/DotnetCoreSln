﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SecurityDemo.Services.Policies
{
    public class MinAgeHandler : AuthorizationHandler<MinAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinAgeRequirement requirement)
        {
            bool hasClaim = context.User.HasClaim(c => c.Type == "age");
            bool hasIdentity = context.User.Identities.Any(i => i.AuthenticationType == "MyCookieMiddlewareInstance");
            string claimValue = context.User.FindFirst(c => c.Type == "age")?.Value;

            if (!string.IsNullOrEmpty(claimValue) && int.Parse(claimValue) > requirement.Age)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}