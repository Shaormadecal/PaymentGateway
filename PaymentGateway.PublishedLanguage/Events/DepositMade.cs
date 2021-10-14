using MediatR;

namespace PaymentGateway.PublishedLanguage.Events
{
   public class DepositMade : INotification
    {
        public string Iban { get; set; }
        public double Amount { get; set; }
        public string Name { get; set; }
    }
}
