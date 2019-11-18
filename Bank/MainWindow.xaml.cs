using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Bank
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Customer customer;
        SavingAccount savingAccount;
        CheckingAccount checkingAccount;
        RetirementAccount retirementAccount;
        CustomerList customerList;

        BankAccount bankAccount; //active account
        Transaktion transaktion;

        public MainWindow()
        {
            InitializeComponent();
            customerList = new CustomerList();
        }
        private void MakeCustomer(string f, string l, int t)
        {
            customer = new Customer(f, l, t);
            customerList.AddCustomer(customer);

            MessageBox.Show("Du blir registerad!");
        }
        private void ShowCustomers()
        {
            comboCustomers.Items.Clear();
            foreach (var customer in customerList.Customers)
            {
                comboCustomers.Items.Add(customer.GetFullName());
            }
        }
        private void ShowAccounts()
        {
            comboAccounts.Items.Clear();
            foreach (var customer in customerList.Customers)
            {
                if (comboCustomers.SelectedItem.Equals(customer.GetFullName()))
                {
                    foreach (var account in customer.BankAccounts)
                    {
                        comboAccounts.Items.Add($"No.{account.AccountNumber}  {account.AccountType}: { account.GetBalanceAndCredit()} kr");
                    }
                }
            }

        }

        private void MakeAccount()
        {
            foreach(var c in customerList.Customers)
            {
              
                if (comboCustomers.SelectedItem.Equals(c.GetFullName()))
                {
                    
                    if((rbtnC.IsChecked == true)&&(boxCredit.Text!=""))
                    {
                        int banknumber = c.AccountNumber();
                        checkingAccount = new CheckingAccount(int.Parse(boxCredit.Text));
                        c.AddBankAccount(checkingAccount);
                        checkingAccount.AddAccountNumber(banknumber);

                        MessageBox.Show("Du har skapat ett lönekonto!");
                    }

                    if (rbtnR.IsChecked == true)
                    {
                        int banknumber = c.AccountNumber();
                        retirementAccount = new RetirementAccount();
                        c.AddBankAccount(retirementAccount);
                        retirementAccount.AddAccountNumber(banknumber);

                        MessageBox.Show("Du har skapat ett pensionsspar!");
                    }
                    if (rbtnS.IsChecked == true)
                    {
                        int banknumber = c.AccountNumber();
                        savingAccount = new SavingAccount();
                        c.AddBankAccount(savingAccount);
                        savingAccount.AddAccountNumber(banknumber);

                        MessageBox.Show("Du har skapat ett sparkonto!");
                    }
                }
            }


        }
        private void SelectedBank()
        {
            foreach(var c in customerList.Customers)
            {
                if (comboCustomers.SelectedItem.Equals(c.GetFullName()))
                {
                    foreach (var a in c.BankAccounts)
                    {
                        if (comboAccounts.SelectedItem.Equals($"No.{a.AccountNumber}  {a.AccountType}: { a.GetBalanceAndCredit()} kr"))
                        {
                            bankAccount = a;
                        }
                    }
                }
            }
        }
        private void StartDeposit(int input)
        {
            bankAccount.CalcDeposit(input);
            RegisterLog("Insättning", input);
            PopupResult();
        }
        private void StartWithDraw(int input)
        {
            bool drawSwitch =bankAccount.ConfirmSetWithDraw(input);
            if (drawSwitch == true) 
            { 
                bankAccount.CalcDraw(input); 
                SetBackBank();
                RegisterLog("Uttag", input);
                PopupResult();
            }
            else if (drawSwitch == false)
            {
                if (bankAccount.CommissionPaid> 0) { MessageBox.Show("Du kan inte dra mer än saldo. Tänk på uttagsavgift också."); }
                else { MessageBox.Show("Du kan inte dra mer än saldo"); }
            }
        }
        private void SetBackBank()
        {
            int counter = 0;// räknare för index: List<BankAccount>
            foreach (var c in customerList.Customers)
            {
                if (comboCustomers.SelectedItem.Equals(c.GetFullName()))
                {
                    
                    foreach (var a in c.BankAccounts)
                    {
                        if (comboAccounts.SelectedItem.Equals($"No.{a.AccountNumber}  {a.AccountType}: { a.GetBalanceAndCredit()} kr"))
                        {
                            c.BankAccounts[counter] = bankAccount;
                        }
                        counter++;
                    }
                }
            }
        }
        private void RegisterLog(string banktype, int input)
        {
            transaktion = new Transaktion(banktype, input);
            bankAccount.AddTransaktion(transaktion);
        }
        private void ShowLog()
        {
            listLog.Items.Clear();
            foreach (var transaktionHistory in bankAccount.Transaktions)
            {
                listLog.Items.Add($"{transaktionHistory.Date} - {transaktionHistory.Type}: {transaktionHistory.Input}kr");
            }
        }
        private void OpenLog()
        {
            foreach (var c in customerList.Customers)
            {
                if (comboCustomers.SelectedItem.Equals(c.GetFullName()))
                {
                    foreach (var a in c.BankAccounts)
                    {
                        if (comboAccounts.SelectedItem.Equals($"No.{a.AccountNumber}  {a.AccountType}: { a.GetBalanceAndCredit()} kr"))
                        {
                            ShowLog();
                        }
                    }
                }
            }
        }
        private void ResetCombo()
        {
            comboAccounts.SelectedIndex=-1;
            comboCustomers.SelectedIndex = -1;
        }
        private void PopupResult()
        {
            MessageBox.Show($"Klart! Du gjorde {transaktion.Type}: {transaktion.Input} kr.");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if ((boxFname.Text=="")|| (boxLname.Text == "")){ MessageBox.Show("Du saknar täckning på kontot."); }
            else if (boxTel.Text == "") { MessageBox.Show("Du saknar täckning på kontot."); }
            else
            {
                MakeCustomer(boxFname.Text, boxLname.Text, int.Parse(boxTel.Text));
                ShowCustomers();
                ResetCombo();

                boxInput.Clear();
                listLog.Items.Clear();
                boxFname.Clear();
                boxLname.Clear();
                boxTel.Clear();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (comboCustomers.SelectedIndex!= -1) 
            {
                MakeAccount();
                bankAccount = new BankAccount();

                comboAccounts.SelectedIndex = -1;
                ShowAccounts();
                boxCredit.Text="0";
            }
        }

        private void rbtnS_Checked(object sender, RoutedEventArgs e)
        {
            boxCredit.IsEnabled = false;
        }

        private void rbtnR_Checked(object sender, RoutedEventArgs e)
        {
            boxCredit.IsEnabled = false;
        }

        private void rbtnC_Checked(object sender, RoutedEventArgs e)
        {
            if (boxCredit != null) { boxCredit.IsEnabled = true; }
                
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if ((comboCustomers.SelectedIndex != -1) && (comboAccounts.SelectedIndex != -1)) 
            {
                bankAccount = new BankAccount();
                SelectedBank();

                if ((rbtnDeposit.IsChecked == true)&&(boxInput.Text!=""))
                {
                    StartDeposit(int.Parse(boxInput.Text));
                    SetBackBank();
                }
                else if ((rbtnWithDraw.IsChecked == true)&&(boxInput.Text!=""))
                {
                    StartWithDraw(int.Parse(boxInput.Text));
                }

                ShowAccounts();
                
                boxInput.Text = "0";
                listLog.Items.Clear();
            }

        }

        private void comboCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboCustomers.SelectedIndex != -1) 
            {
                ShowAccounts();
            }
        }

        private void comboAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if ((comboCustomers.SelectedIndex != -1) && (comboAccounts.SelectedIndex != -1)) 
            {
                SelectedBank();
                OpenLog();
            }
        }
        private void boxCredit_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !new Regex("[0-9]").IsMatch(e.Text);
        }

        private void boxInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !new Regex("[0-9]").IsMatch(e.Text);
        }

        private void boxTel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !new Regex("[0-9]").IsMatch(e.Text);
        }
    }
}
