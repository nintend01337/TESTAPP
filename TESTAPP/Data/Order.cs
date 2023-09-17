using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTAPP.Data
{
    public class Order
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }

        public Order(string title, DateTime date, double amount)
        {
            Title = title;
            Date = date;
            Amount = amount;
        }
    }
}