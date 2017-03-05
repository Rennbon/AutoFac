using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class StandardCard : CreditCard
    {
        public StandardCard(string accountId) : base(accountId)
        {
        }
        public override void WhatIsIt()
        {
            Console.WriteLine("StandardCard");
        }
    }
}
