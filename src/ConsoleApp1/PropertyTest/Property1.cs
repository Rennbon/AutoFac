using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1.PropertyTest
{
    public class Property1
    {
        public Property1()
        { }
        public Property1(Property2 p)
        {
        }
        public Property2 P2 { set; get; }

        public string S2 { set; get; }

        public void Method(Property2 p)
        {
            Console.WriteLine(p.GetType());
        }
    }
}
