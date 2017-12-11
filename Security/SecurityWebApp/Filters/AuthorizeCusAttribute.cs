using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SecurityWebApp.Filters
{
    public class AuthorizeCusAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly IAuthorizationService _authService;
        private readonly AuthorizationRequirementCus _authorizationRequirementCus;
        public AuthorizeCusAttribute(IAuthorizationService authService, AuthorizationRequirementCus authorizationRequirementCus)
        {
            _authService = authService;
            _authorizationRequirementCus = authorizationRequirementCus;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var ok = await _authService.AuthorizeAsync(context.HttpContext.User, null, _authorizationRequirementCus);
            if (!ok.Succeeded) context.Result = new ChallengeResult();
        }
    }
}
