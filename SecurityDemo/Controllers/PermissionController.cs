using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurityDemo.Services.Permissions;
using SecurityDemo.Services.TestDI;
using System.Threading;

namespace SecurityDemo.Controllers
{
    [Route("api/[controller]")]
    public class PermissionController : Controller
    {
        private readonly ITest _test;
        public PermissionController(ITest test)
        {
            _test = test;
        }
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
            var claim = HttpContext.User.FindFirst("abc");
            var hearder = Request.Headers["user-agent"].ToString();
            _test.Hello();
            return Ok("OK");
        }
    }
}