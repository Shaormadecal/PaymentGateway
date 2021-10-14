using PaymentGateway.Data;
using System.Collections.Generic;
using System.Linq;

namespace PaymentGateway.Application.Services
{
    public class NewIban
    {
        private readonly Database _database;

        public NewIban(Database database)
        {
            _database = database;
        }

        public string GetNewIban()
        {
            List<string> ibans = _database.BankAccounts.Select(x => x.Iban).ToList();
           
            if (ibans.Count == 0)
                return "1";

            return (long.Parse(ibans.Last()) + 1).ToString();
        }
    }
}
