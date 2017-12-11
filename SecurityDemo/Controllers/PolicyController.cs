using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StepByStepReact.Controllers
{
    [Route("api/[controller]")]
    public class PolicyController : Controller
    {
        [Authorize(Policy = "age-adult-policy")]
        public IActionResult Allow()
        {
            return Ok("OK");
        }

        [Authorize(Policy = "age-elder-policy")]
        public IActionResult Deny()
        {
            return Ok("OK");
        }
    }
}
