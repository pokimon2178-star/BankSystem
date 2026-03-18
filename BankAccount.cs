using System;

namespace BankSystem
{
    public class BankAccount : AbstractAccount
    {
        public BankAccount(string owner, decimal initialBalance, Currency currency, string id = null, AccountStatus status = AccountStatus.Active)
        {
            AccountId = string.IsNullOrEmpty(id) ? Guid.NewGuid().ToString().Substring(0, 8) : id;
            OwnerData = owner;
            _balance = initialBalance;
            AccountCurrency = currency;
            Status = status;
        }

        private void CheckStatus()
        {
            if (Status == AccountStatus.Frozen) throw new AccountFrozenError("Счет заморожен!");
            if (Status == AccountStatus.Closed) throw new AccountClosedError("Счет закрыт!");
        }

        public override void Deposit(decimal amount)
        {
            CheckStatus();
            if (amount <= 0) throw new InvalidOperationError("Сумма должна быть положительной.");
            _balance += amount;
            Console.WriteLine($"Баланс пополнен на {amount}. Текущий баланс: {_balance}");
        }

        public override void Withdraw(decimal amount)
        {
            CheckStatus();
            if (amount <= 0) throw new InvalidOperationError("Сумма должна быть положительной.");
            if (amount > _balance) throw new InsufficientFundsError("Недостаточно средств.");
            _balance -= amount;
            Console.WriteLine($"Снято {amount}. Текущий баланс: {_balance}");
        }

        public override string GetAccountInfo()
        {
            string lastFour = AccountId.Length >= 4 ? AccountId.Substring(AccountId.Length - 4) : AccountId;
            return $"\n--- ИНФО ---\nТип: BankAccount\nКлиент: {OwnerData}\nID: ***{lastFour}\nСтатус: {Status}\nБаланс: {_balance} {AccountCurrency}\n------------";
        }

        public override string ToString() => GetAccountInfo();
    }
}