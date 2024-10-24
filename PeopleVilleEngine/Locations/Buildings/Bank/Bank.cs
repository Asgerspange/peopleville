namespace PeopleVilleEngine.Locations.Buildings.Bank
{
    internal class Bank
    {
        private Dictionary<int, BankAccount> accounts = new Dictionary<int, BankAccount>();
        private int nextAccountNumber = 1;

        public int CreateAccount(string accountHolder)
        {
            var account = new BankAccount
            {
                AccountNumber = nextAccountNumber++,
                AccountHolder = accountHolder,
                Balance = 0.0m
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
    }
}
