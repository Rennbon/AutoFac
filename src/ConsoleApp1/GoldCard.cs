using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class GoldCard : CreditCard
    {
        public GoldCard(string accountId) : base(accountId)
        {
        }

        public override void WhatIsIt()
        {
            Console.WriteLine("GoldCard");
        }

    }
}
