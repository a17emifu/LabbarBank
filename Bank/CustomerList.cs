using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class CustomerList
    {
        public List<Customer> Customers { get; private set; }

        public CustomerList()
        {
            Customers = new List<Customer>();
        }
        public void AddCustomer(Customer c)
        {
            Customers.Add(c);
        }
    }
}
