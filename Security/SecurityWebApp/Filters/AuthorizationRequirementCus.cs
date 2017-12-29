using Microsoft.AspNetCore.Authorization;
namespace SecurityWebApp.Filters
{
    public enum OpCRUD
    {
        Create = 1,
        Read = 2,
        Update = 3, 
        Delete = 4
    }
    public class AuthorizationRequirementCus : IAuthorizationRequirement
    {
        public string DocumentId { get; set; }
        public OpCRUD[] Op { get; set; }
        public AuthorizationRequirementCus(string documentId, OpCRUD[] op)
        {
            DocumentId = documentId;
            Op = op;
        }
    }
}
