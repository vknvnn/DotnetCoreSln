using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StepByStepReact.Services.Permissions;
namespace StepByStepReact.Backend.Permission.Controllers
{
    public class PermissionController : Controller
    {
        [TypeFilter(typeof(PermissionFilterV1),
            Arguments = new object[] { new[] { Services.Permissions.Permission.Foo, Services.Permissions.Permission.Bar } })]
        public IActionResult ExampleV1()
        {
            return View("OK");
        }

        [Services.Permissions.AuthorizePermission(Services.Permissions.Permission.Foo, Services.Permissions.Permission.Bar)]
        [Authorize(Policy = "age-adult-policy")]
        public IActionResult ExampleV2()
        {
            return View("OK");
        }
    }
}