using System;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace ContextBase
{
    public struct JwtCustomizeClaimNames
    {
        public const string Tid = "tid";
        public const string Ofs = "ofs";
    }

    public interface ITenantFactory
    {        
        Guid GetTenantId();
        int GetClientOffset();
        string GetUserName();
    }
    public class TenantFactory : ITenantFactory
    {
        private Guid _tenantId;
        private int _clientTime;
        private string _userName;
        public TenantFactory(HttpContext httpContext)
        {
            if (httpContext != null)
            {
                var claim = httpContext.User.FindFirst(JwtCustomizeClaimNames.Tid);
                if (claim != null)
                {
                    _tenantId = Guid.Parse(claim.Value);
                }
                claim = httpContext.User.FindFirst(JwtCustomizeClaimNames.Ofs);
                if (claim != null)
                {
                    int.TryParse(claim.Value, out _clientTime);
                }
                claim = httpContext.User.FindFirst(JwtRegisteredClaimNames.Sub);
                if (claim != null)
                {
                    _userName = claim.Value;
                }
            }
        }
       
        public int GetClientOffset()
        {
            return _clientTime;
        }
        
        public Guid GetTenantId()
        {
            return _tenantId;
        }

        public string GetUserName()
        {
            return _userName;
        }
    }
}
