using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SecurityWebApp.TokenHelper;

namespace StepByStepReact.Backend.Token
{
    public class LoginInputModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
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
                                .AddSecurityKey(JwtSecurityKey.Create("fiver-secret-key"))
                                .AddSubject("james bond")
                                .AddIssuer("Fiver.Security.Bearer")
                                .AddAudience("Fiver.Security.Bearer")
                                .AddClaim("MembershipId", "111")
                                .AddExpiry(1)
                                .Build();

            //return Ok(token);
            return Ok(token.Value);
        }
    }
}