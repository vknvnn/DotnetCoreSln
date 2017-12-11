using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SecurityWebApp.Filters
{
    public class AuthorizationHandlerCus : AuthorizationHandler<AuthorizationRequirementCus>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationRequirementCus requirement)
        {
            //Claim claim = context.User.FindFirst(c => c.Type == "permission-foo");

            //if (requirement.DocumentId.Any() && claim != null) //TODO: your code
            //{
            //    context.Succeed(requirement);
            //}
            //Step 1 get claim;
            //Step 2 check cache; Check DocumentId and Operations
            //Step 3 
            return Task.CompletedTask;
        }
    }    
}
