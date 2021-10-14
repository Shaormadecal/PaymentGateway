namespace PaymentGateway.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public double Balance { get; set; }
        public string Currency { get; set; }
        public string Iban { get; set; }
        public string Status { get; set; }
        public double Limit { get; set; }
        public string Type { get; set; }

    }
}
