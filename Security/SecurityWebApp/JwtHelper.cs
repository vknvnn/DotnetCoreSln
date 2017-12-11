using Microsoft.IdentityModel.Tokens;
using SecurityWebApp.TokenHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;

namespace SecurityWebApp
{
    public delegate void CallBackAuthenticationFailed(AuthenticationFailedContext context);
    public delegate void CallBackTokenValidated(TokenValidatedContext context);
    public static class JwtHelper
    {
        public static TokenValidationParameters GetTokenValidation(string iss, string aud, string secret)
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = iss,
                ValidAudience = aud,
                IssuerSigningKey = JwtSecurityKey.Create(secret)
            };
        }

        public static JwtBearerEvents GetTokenEvent(CallBackTokenValidated callBackValid = null, CallBackAuthenticationFailed callBackFailed = null)
        {
            return new JwtBearerEvents {
                OnAuthenticationFailed = context =>
                {
                    callBackFailed?.Invoke(context);
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    //context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    //context.Response.Headers.Append("error_description", "The token is expired");
                    //context.Fail("The token is expired");
                    callBackValid?.Invoke(context);
                    return Task.CompletedTask;
                },
                
            };
        }
    }
}
