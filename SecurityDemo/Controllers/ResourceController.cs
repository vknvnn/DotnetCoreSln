using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurityDemo.Models;
using SecurityDemo.Services.Resources;

namespace SecurityDemo.Controllers
{
    [Route("api/[controller]")]
    public class ResourceController : Controller
    {
        IAuthorizationService _authorizationService;

        public ResourceController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> ExampleV1(int id)
        {
            Order order = new Order(); //get resourse from DB
            var ok = await _authorizationService.AuthorizeAsync(this.User, order, "resource-allow-policy");
            if (ok.Succeeded)
            {
                return Ok("OK");
            }

            return new ChallengeResult(); //it produces 401 or 403 response (depending on user state)
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> ExampleV2()
        {
            var requirements = new[] { Operations.Create, Operations.Read, Operations.Update };
            var ok = await _authorizationService.AuthorizeAsync(this.User, new Order(), requirements);
            if (ok.Succeeded)
            {
                return Ok("OK");
            }

            return new ChallengeResult();  //it produces 401 or 403 response (depending on user state)
        }
    }
}
