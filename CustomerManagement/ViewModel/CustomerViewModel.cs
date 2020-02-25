using CustomerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagement.ViewModel
{
    public class CustomerViewModel
    {
        public List<Customer> Customers { get; set; }
        public Customer Customer { get; set; }
        public CustomerViewModel()
        {
            this.Customer = new Customer();
            this.Customers = new List<Customer>();
        }
    }
}