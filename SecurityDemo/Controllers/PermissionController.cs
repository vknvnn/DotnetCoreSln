using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurityDemo.Services.Permissions;

namespace SecurityDemo.Controllers
{
    [Route("api/[controller]")]
    public class PermissionController : Controller
    {
        [TypeFilter(typeof(PermissionFilterV1),
            Arguments = new object[] { new[] { Permission.Foo, Permission.Bar } })]
        [HttpGet("[action]")]
        public IActionResult ExampleV1()
        {
            return Ok("OK");
        }

        [AuthorizePermission(Permission.Foo, Permission.Bar)]
        //[Authorize(Policy = "age-adult-policy")]
        [HttpGet("[action]")]
        public IActionResult ExampleV2()
        {
            return Ok("OK");
        }
    }
}