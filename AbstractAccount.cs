namespace BankSystem
{
    public abstract class AbstractAccount
    {
        public string AccountId { get; protected set; } = string.Empty;
        public string OwnerData { get; protected set; } = string.Empty;
        protected decimal _balance;
        public AccountStatus Status { get; set; }
        public Currency AccountCurrency { get; protected set; }

        public abstract void Deposit(decimal amount);
        public abstract void Withdraw(decimal amount);
        public abstract string GetAccountInfo();
    }
}