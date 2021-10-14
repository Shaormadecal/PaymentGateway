using PaymentGateway.Models;
using System;
using System.Collections.Generic;

namespace PaymentGateway.Data
{
    public class Database
    {
        public List<Person> Persons = new List<Person>();
        public List<Product> Products = new List<Product>();
        public List<BankAccount> BankAccounts = new List<BankAccount>();
        public List<Transaction> Transactions = new List<Transaction>();
        public List<ProductXTransaction> ProductXTransaction = new List<ProductXTransaction>();

        //private static Database instance;
        //public static Database GetInstance()
        //{
        //    if (instance == null) instance = new Database();
        //    return instance;
        //}

        public void SaveChanges()
        {
            Console.WriteLine("Changes Saved.....");
        }
    }
}
