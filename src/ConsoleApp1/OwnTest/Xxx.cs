using Autofac.Features.OwnedInstances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1.OwnTest
{
    public class Xxx
    {
        private readonly Owned<Ooo> _xxx;

        public Xxx(Owned<Ooo> hhh)
        {
            _xxx = hhh;
        }
        public void Dispose()
        {
            _xxx.Dispose();
        }
    }
}
