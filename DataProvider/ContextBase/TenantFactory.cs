using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using System.Threading;

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
        
    }
}
