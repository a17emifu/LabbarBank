using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class Customer
    {
        int Cellphone;
        string FirstName;
        string LastName;
        public List<BankAccount> BankAccounts { get; private set; }
        public AccountNumberManager numberManager { get; private set; }
        public Customer(string fname, string lname, int tel)
        {
            FirstName = fname;
            LastName = lname;
            Cellphone = tel;
            BankAccounts = new List<BankAccount>();
            numberManager = new AccountNumberManager();
        }
        public string GetFullName()
        {
            string fullname = $"{FirstName} {LastName}";
            return fullname;
        }
        public void AddBankAccount(BankAccount a)
        {
            BankAccounts.Add(a);
        }
        public int AccountNumber()
        {
            numberManager.AddAccountNumber();
            return numberManager.AccountNumber;
        }
    }
}
