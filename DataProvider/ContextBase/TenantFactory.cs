using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Http;

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
        void Load(HttpContext httpContext);
    }
    public class TenantFactory : ITenantFactory
    {
        public TenantFactory()
        {
            
        }
        private Guid _tenantId;
        private int _clientTime;

        public int GetClientOffset()
        {
            return _clientTime;
        }
        
        public Guid GetTenantId()
        {
            return _tenantId;
        }

        public void Load(HttpContext httpContext)
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
            }
        }
    }
}
