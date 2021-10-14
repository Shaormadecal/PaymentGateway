using System;

namespace PaymentGateway.Models
{
    public class Transaction
    {
        public int ID { get; set; }

        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }

    }
}
