using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SecurityWebApp.TokenHelper;
using System;

namespace SecurityDemo.Controllers
{
    public class LoginInputModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Offset { get; set; }
    }

    [Route("[controller]")]
    [AllowAnonymous]
    public class TokenController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody]LoginInputModel inputModel)
        {            
            if (inputModel.Username != "james" && inputModel.Password != "bond")
                return Unauthorized();
            var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create("nghiepvo-secret-key"))
                                .AddSubject("james bond")
                                .AddIssuer("nghiepvo.com")
                                .AddAudience("nghiepvo.com")
                                .AddTenant(Guid.NewGuid().ToString()) // Get from db.
                                .AddOffset(inputModel.Offset)
                                .AddClaim("MembershipId", "111")
                                .AddClaim("permission-foo", "111")
                                .AddExpiry(3600)
                                .Build();
           
            return Ok(token.Value);
        }
    }
}