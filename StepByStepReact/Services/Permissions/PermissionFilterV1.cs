﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StepByStepReact.Services.Permissions
{
    public class PermissionFilterV1 : Attribute, IAsyncAuthorizationFilter
    {
        private readonly IAuthorizationService _authService;
        private readonly Permission[] _permissions;

        public PermissionFilterV1(IAuthorizationService authService, Permission[] permissions)
        {
            _authService = authService;
            _permissions = permissions;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var ok = await _authService.AuthorizeAsync(context.HttpContext.User, null, new PermissionRequirement(_permissions));

            if (!ok.Succeeded) context.Result = new ChallengeResult();
        }
    }
}