using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class AccountNumberManager
    {
        public int AccountNumber { get; private set; }
        public AccountNumberManager()
        {
            AccountNumber = 0;
        }
        public void AddAccountNumber()
        {
            AccountNumber++;
        }
    }
}
