public class Wallet : IWallet
{
    public decimal Money { get; private set; }
    public string Currency { get; private set; }

    public Wallet(string currency, decimal initialAmount = 0m)
    {
        if (initialAmount < 0) throw new ArgumentException("Initial amount cannot be negative!");
        Money = initialAmount; // Initialiserer med startbeløb
        Currency = currency; // Sætter valuta til $
    }

    public void AddMoney(decimal amount)
    {
        if (amount < 0) throw new ArgumentException("Beløb kan ikke være negativt!");
        Money += amount;
    }

    public bool SpendMoney(decimal amount)
    {
        if (amount < 0) throw new ArgumentException("Beløb kan ikke være negativt!");
        if (Money >= amount)
        {
            Money -= amount;
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"{Money} {Currency}";
    }
}
