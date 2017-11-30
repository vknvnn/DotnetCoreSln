using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StepByStepReact.Controllers
{
    public class PolicyController : Controller
    {
        [Authorize(Policy = "age-adult-policy")]
        public IActionResult Allow()
        {
            return View("OK");
        }

        [Authorize(Policy = "age-elder-policy")]
        public IActionResult Deny()
        {
            return View("OK");
        }
    }
}
