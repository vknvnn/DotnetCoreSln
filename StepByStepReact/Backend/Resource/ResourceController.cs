using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StepByStepReact.Models;
using StepByStepReact.Services.Resources;

namespace StepByStepReact.Controllers
{
    public class ResourceController : Controller
    {
        IAuthorizationService _authorizationService;

        public ResourceController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> ExampleV1(int id)
        {
            Order order = new Order(); //get resourse from DB
            var ok = await _authorizationService.AuthorizeAsync(this.User, order, "resource-allow-policy");
            if (ok.Succeeded)
            {
                return View("OK");
            }

            return new ChallengeResult(); //it produces 401 or 403 response (depending on user state)
        }

        public async Task<IActionResult> ExampleV2()
        {
            var requirements = new [] { Operations.Create, Operations.Read, Operations.Update };
            var ok = await _authorizationService.AuthorizeAsync(this.User, new Order(), requirements);
            if (ok.Succeeded)
            {
                return View("OK");
            }

            return new ChallengeResult();  //it produces 401 or 403 response (depending on user state)
        }
    }
}
