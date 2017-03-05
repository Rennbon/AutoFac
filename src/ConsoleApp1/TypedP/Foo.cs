using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1.TypedP
{
    public class Foo
    {
        IEnumerable<IRule> rules;
        public Foo(IEnumerable<IRule> rules)
        {
            this.rules = rules;
        }

        public int GetNumberOfRules()
        {
            return this.rules.Count();
        }
    }

    public interface IRule
    {
    }

    public class Rule1 : IRule
    {
    }

    public class Rule2 : IRule
    {
    }
}
