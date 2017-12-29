using System;
using Microsoft.AspNetCore.Mvc;

namespace SecurityWebApp.Filters
{
    public class AuthorizeCusAttribute : TypeFilterAttribute
    {
        public AuthorizeCusAttribute(string documentId, params OpCRUD[] op) : base(typeof(AuthorizationFilterCus))
        {
            Arguments = new[] { new AuthorizationRequirementCus(documentId, op) };
            Order = Int32.MinValue;
        }
    }
}
