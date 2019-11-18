using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class Transaktion
    {
        public DateTime Date;
        public string Type{ get; private set; }
        public int Input { get; private set; }

        public Transaktion(string transaktiontype, int input)
        {
            Date = DateTime.Now;
            Type = transaktiontype;
            Input = input;
        }
    }
}
