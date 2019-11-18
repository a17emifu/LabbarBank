using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class BankAccount
    {
        public string AccountType;
        protected int Balance;
        protected int Credit;
        public double CommissionPaid { get; protected set; }
        public int AccountNumber { get; private set; }
        public List<Transaktion> Transaktions { get; private set; }

        public BankAccount()
        {
            Transaktions = new List<Transaktion>();
            Balance = 0;
        }
       public void AddAccountNumber(int accountid)
        {
            AccountNumber = accountid;
        }
        public void CalcDeposit(int a)
        {
            Balance = Balance + a;
        }

        public bool ConfirmSetWithDraw(int b)
        {
            int drawAll = b + SetFee();
            bool KanDraw;
            if (drawAll > Balance+Credit)
            {
                KanDraw = false;
            }
            else { KanDraw = true; }
            return KanDraw;
        }

        public void CalcDraw(int b)
        {

            int drawAll = b + SetFee();
            Balance = Balance - drawAll;
        }

        public double GetBalanceAndCredit()
        {
            double saldo = Balance + Credit;
            return Math.Round(saldo,2);
        }

        public int SetFee()
        {
            double fee;
            fee = Balance * CommissionPaid;
            return (int)fee;
        }
        public void AddTransaktion(Transaktion transaktion) 
        {
            Transaktions.Add(transaktion);
        }
    }
}
