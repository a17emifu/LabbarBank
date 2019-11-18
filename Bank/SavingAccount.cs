using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class SavingAccount:BankAccount
    {
        public SavingAccount()
        {
            AccountType = "Sparkonto";
            CommissionPaid = 0;
        }
    }
}
