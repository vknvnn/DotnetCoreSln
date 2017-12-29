using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurityWebApp.Filters;

namespace SecurityDemo.Controllers
{
    
    [Route("api/[controller]")]
    public class TokenInfosController : Controller
    {
        [HttpGet]
        [AuthorizeCus("TokenInfos", OpCRUD.Read)]
        public IActionResult Get()
        {
            var dict = new Dictionary<string, string>();

            HttpContext.User.Claims.ToList()
               .ForEach(item => dict.Add(item.Type, item.Value));

            return Ok(dict);
        }
    }
}
