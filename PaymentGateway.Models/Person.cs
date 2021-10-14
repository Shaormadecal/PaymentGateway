using System;
using System.Collections.Generic;

namespace PaymentGateway.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public String Cnp { get; set; }
        public PersonType TypeOfPerson { get; set; }

        public List<BankAccount> Accounts { get; set; } = new List<BankAccount>();
    }
}
