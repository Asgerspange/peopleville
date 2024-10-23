public interface IWallet
{
    decimal Money { get; }
    string Currency { get; }
    void AddMoney(decimal amount);
    bool SpendMoney(decimal amount);
    string ToString();
}
