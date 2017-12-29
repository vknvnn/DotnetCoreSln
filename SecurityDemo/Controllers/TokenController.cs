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
            if (inputModel.Username != inputModel.Password)
                return Unauthorized();
            var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create("nghiepvo-secret-key"))
                                .AddSubject(inputModel.Username)
                                .AddIssuer("nghiepvo.com")
                                .AddAudience("nghiepvo.com")
                                .AddTenant(Guid.NewGuid().ToString()) // Get from db.
                                .AddOffset(inputModel.Offset)                                
                                .AddExpiry(3600)
                                .Build();           
            return Ok(token.Value);
        }
    }
}