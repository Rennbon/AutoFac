using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public abstract class CreditCard
    {
        public CreditCard(string accountId)
        {
        }
        public abstract void WhatIsIt();
    }
}
