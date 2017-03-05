using Autofac.Features.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class A
    {
        private string name;
        public A(string name)
        {
            this.name = name;
        }
        public void Write()
        {
            Console.WriteLine(name);
        }
    }
}
