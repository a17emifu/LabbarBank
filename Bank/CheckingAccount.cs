using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class CheckingAccount: BankAccount
    {
        public CheckingAccount(int credit)
        {
            AccountType = "Lönekonto";
            Credit = credit;
            CommissionPaid = 0;
        }
    }
}
