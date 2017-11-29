using System;
using Microsoft.AspNetCore.Mvc;


namespace SecurityWebApp.Filters
{
    public class HMACAuthenticationAttribute : TypeFilterAttribute
    {
        public HMACAuthenticationAttribute(Type type) : base(type)
        {

        }
    }


}
