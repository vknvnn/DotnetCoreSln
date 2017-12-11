using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityDemo.Services.TestDI
{
    public interface ITest
    {
        void Hello();
    }
    public class Test : ITest
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Test(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void Hello()
        {
            Console.WriteLine("Hello");
        }
    }
}
