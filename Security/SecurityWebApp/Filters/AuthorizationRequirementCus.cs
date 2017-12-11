using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityWebApp.Filters
{
    public static class OperationAuthorizationRequirementCus
    {
        public static OperationAuthorizationRequirement Create =
            new OperationAuthorizationRequirement { Name = "Create" };
        public static OperationAuthorizationRequirement Read =
            new OperationAuthorizationRequirement { Name = "Read" };
        public static OperationAuthorizationRequirement Update =
            new OperationAuthorizationRequirement { Name = "Update" };
        public static OperationAuthorizationRequirement Delete =
            new OperationAuthorizationRequirement { Name = "Delete" };
    }
    public class AuthorizationRequirementCus : IAuthorizationRequirement
    {
        public string DocumentId { get; set; }
        public OperationAuthorizationRequirement[] Op { get; set; }
        public AuthorizationRequirementCus(string documentId, OperationAuthorizationRequirement[] op)
        {
            DocumentId = documentId;
            Op = op;
        }
    }
}
