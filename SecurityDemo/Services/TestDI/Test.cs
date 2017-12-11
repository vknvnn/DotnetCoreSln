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
        public void Hello()
        {
            Console.WriteLine("Hello");
        }
    }
}
