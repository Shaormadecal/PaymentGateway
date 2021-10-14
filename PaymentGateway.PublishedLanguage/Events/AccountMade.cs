using MediatR;

namespace PaymentGateway.PublishedLanguage.Events
{
    public class AccountMade: INotification
    {
        public string Name { get; set; }
    }
}