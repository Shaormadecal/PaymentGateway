using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.WebApi.CommandHandler
{
    public class GenericCommandHandler : MediatR.IRequestHandler<MediatR.IRequest>
    {
        public Task<Unit> Handle(IRequest request, CancellationToken cancellationToken)
        {
            // send to queue
            Console.WriteLine($"sending to queue {request}");
            return Unit.Task;
        }
    }
}