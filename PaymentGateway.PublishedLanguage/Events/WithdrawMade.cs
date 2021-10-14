using MediatR;

namespace PaymentGateway.PublishedLanguage.Events
{
   public class WithdrawMade : INotification
    {
        public string Iban { get; set; }
        public double Amount { get; set; }
    }
}