﻿using Microsoft.AspNetCore.Authorization;

namespace StepByStepReact.Services.Permissions
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(Permission[] permissions)
        {
            Permissions = permissions;
        }
        public Permission[] Permissions { get; set; }
    }
}