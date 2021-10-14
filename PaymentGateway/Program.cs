using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Application;
using PaymentGateway.Application.Queries;
using PaymentGateway.Application.Services;
using PaymentGateway.Data;
using PaymentGateway.ExternalService;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway
{
    class Program
    {
        static IConfiguration Configuration;
        static async Task Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // setup
            var services = new ServiceCollection();
            var source = new CancellationTokenSource();
            var cancellationToken = source.Token;
            services.AddMediatR(typeof(ListOfAccounts).Assembly, typeof(AllEventsHandler).Assembly);
            services.RegisterBusinessServices(Configuration);

            //services.AddSingleton<IEventSender, EventSender>();
            services.AddSingleton(Configuration);

            // build
            var serviceProvider = services.BuildServiceProvider();
            var database = serviceProvider.GetRequiredService<Database>();
            var ibanService = serviceProvider.GetRequiredService<NewIban>();
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            // use
            var enrollCustomer = new EnrollCustomer
            {
                ClientType = "Company",
                AccountType = "Debit",
                Name = "Gigi Popa",
                Valuta = "Eur",
                UniqueIdentifier = "23"
            };

            //var enrollCustomerOperation = serviceProvider.GetRequiredService<EnrollCustomerOperation>();
            //enrollCustomerOperation.Handle(enrollCustomer, default).GetAwaiter().GetResult();
            await mediator.Send(enrollCustomer,cancellationToken);
            var makeAccountDetails = new MakeNewAccount
            {
                UniqueIdentifier = "23",
                AccountType = "Debit",
                Valuta = "Eur"
            };
            //var makeAccountOperation = serviceProvider.GetRequiredService<CreateAccount>();
            //makeAccountOperation.Handle(makeAccountDetails, default).GetAwaiter().GetResult();
            await mediator.Send(makeAccountDetails, cancellationToken);


            var makeNewDeposit = new MakeNewDeposit
            {
                Iban = (Int64.Parse(ibanService.GetNewIban()) - 1).ToString(),
                Cnp = "23",
                Currency = "Eur",
                Amount = 750
            };

            //var makeDeposit = serviceProvider.GetRequiredService<DepositMoney>();
            //makeDeposit.Handle(depositDetails, default).GetAwaiter().GetResult();
            await mediator.Send(makeNewDeposit, cancellationToken);

            var makeWithdraw = new MakeWithdraw
            {
                Amount = 150,
                Cnp = "23",
                Iban = (long.Parse(ibanService.GetNewIban()) - 1).ToString()
            };

            //var makeWithdraw = serviceProvider.GetRequiredService<WithdrawMoney>();
            //makeWithdraw.Handle(withdrawDetails, default).GetAwaiter().GetResult();
            await mediator.Send(makeWithdraw, cancellationToken);

            var produs = new Product
            {
                Id = 1,
                Limit = 10,
                Name = "Pantofi",
                Currency = "Eur",
                Value = 10
            };

            var produs1 = new Product
            {
                Id = 2,
                Limit = 5,
                Name = "pantaloni",
                Currency = "Eur",
                Value = 5
            };

            var produs2 = new Product
            {
                Id = 3,
                Limit = 3,
                Name = "Camasa",
                Currency = "Eur",
                Value = 3
            };

            database.Products.Add(produs);
            database.Products.Add(produs1);
            database.Products.Add(produs2);

            var listaProduse = new List<CommandDetails>();

            var prodCmd1 = new CommandDetails
            {
                ProductId = 1,
                Quantity = 2
            };
            listaProduse.Add(prodCmd1);
            var prodCmd2 = new CommandDetails
            {
                ProductId = 2,
                Quantity = 3
            };
            listaProduse.Add(prodCmd2);

            var comanda = new Command
            {
                Details = listaProduse,
                Iban = (Int64.Parse(ibanService.GetNewIban()) - 1).ToString()
            };

            //var purchaseProduct = serviceProvider.GetRequiredService<PurchaseProduct>();
            //purchaseProduct.Handle(comanda, default).GetAwaiter().GetResult();
            await mediator.Send(comanda, cancellationToken);

            var query = new Application.Queries.ListOfAccounts.Query
            {
                PersonId = 1
            };

            //var handler = serviceProvider.GetRequiredService<ListOfAccounts.QueryHandler>();
            //var result = handler.Handle(query, default).GetAwaiter().GetResult();
            var result = await mediator.Send(query, cancellationToken);
        }
    }
}