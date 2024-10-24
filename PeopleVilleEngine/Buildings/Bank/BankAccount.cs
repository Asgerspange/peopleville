using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine.Buildings.Bank
{
    internal class BankAccount
    {
        public int AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public decimal Balance { get; set; }
    }
}
