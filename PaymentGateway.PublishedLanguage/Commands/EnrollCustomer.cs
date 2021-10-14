using MediatR;

namespace PaymentGateway.PublishedLanguage.Commands
{
    public class EnrollCustomer : IRequest
    {
        public string Name { get; set; }
        public string UniqueIdentifier { get; set; }
        public string ClientType { get; set; }

        public string AccountType { get; set; }

        public string Valuta { get; set; }


    }
}
