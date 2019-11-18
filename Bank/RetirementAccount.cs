using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class RetirementAccount: BankAccount
    {
        public RetirementAccount()
        {
            AccountType = "Pensionsspar";
            CommissionPaid = 0.1;
        }

    }
}
