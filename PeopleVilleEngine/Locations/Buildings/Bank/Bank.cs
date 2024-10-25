using PeopleVilleEngine.Villagers;
using System.Security.Principal;

namespace PeopleVilleEngine.Locations.Buildings.Bank
{
    public class Bank : VillagerRoles
    {
        private Dictionary<int, BankAccount> accounts = new Dictionary<int, BankAccount>();
        private int nextAccountNumber = 1;

        public int CreateAccount(string accountHolder, string role)
        {
            var villagerRoles = VillagerRoles.GetInstance();
            decimal startAmount = villagerRoles.GetRoleStartAmount(role);

            var account = new BankAccount
            {
                AccountNumber = nextAccountNumber++,
                AccountHolder = accountHolder,
                Balance = startAmount
            };
            accounts[account.AccountNumber] = account;
            return account.AccountNumber;
        }

        public bool Deposit(int accountNumber, decimal amount)
        {
            if (accounts.TryGetValue(accountNumber, out var account))
            {
                account.Balance += amount;
                return true;
            }
            return false;
        }

        public bool Withdraw(int accountNumber, decimal amount)
        {
            if (accounts.TryGetValue(accountNumber, out var account) && account.Balance >= amount)
            {
                account.Balance -= amount;
                return true;
            }
            return false;
        }

        public decimal GetBalance(int accountNumber)
        {
            if (accounts.TryGetValue(accountNumber, out var account))
            {
                return account.Balance;
            }
            throw new ArgumentException("Account not found.");
        }

        public int PayCheck(decimal paymentAmount, string role)
        {
            var villagerRoles = VillagerRoles.GetInstance();
            decimal amountToAdd = villagerRoles.GetRolePaymentAmount(role);
            int accountsUpdated = 0;

            foreach (var account in accounts.Values)
            {
                if (account.AccountHolder == role)
                {
                    account.Balance += amountToAdd;
                    accountsUpdated++;
                }
            }

            return accountsUpdated;
        }

    }
}
