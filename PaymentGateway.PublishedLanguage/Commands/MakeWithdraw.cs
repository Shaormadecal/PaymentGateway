using System;
using MediatR;

namespace PaymentGateway.PublishedLanguage.Commands
{
    public class MakeWithdraw : IRequest
    {
        public String Cnp { get; set; }
        public string Iban { get; set; }
        public double Amount { get; set; }
    }
}
