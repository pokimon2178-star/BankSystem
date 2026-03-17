using System;

namespace BankSystem
{
    public class AccountFrozenError : Exception { public AccountFrozenError(string m) : base(m) { } }
    public class AccountClosedError : Exception { public AccountClosedError(string m) : base(m) { } }
    public class InvalidOperationError : Exception { public InvalidOperationError(string m) : base(m) { } }
    public class InsufficientFundsError : Exception { public InsufficientFundsError(string m) : base(m) { } }

    public enum AccountStatus { Active, Frozen, Closed }
    public enum Currency { RUB, USD, EUR, KZT, CNY }

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

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Введите имя владельца: ");
                string name = Console.ReadLine() ?? "Unknown";

                Console.Write("Валюта (RUB, USD, EUR, KZT, CNY): ");
                string curInput = Console.ReadLine()?.ToUpper() ?? "RUB";
                Enum.TryParse(curInput, out Currency cur);

                Console.Write("Начальный баланс: ");
                decimal.TryParse(Console.ReadLine(), out decimal bal);

                BankAccount acc = new BankAccount(name, bal, cur);
                Console.WriteLine(acc);

                bool running = true;
                while (running)
                {
                    Console.WriteLine("\nМеню: 1 - Депозит, 2 - Снять, 3 - Заморозить, 4 - Инфо, 0 - Выход");
                    string choice = Console.ReadLine();

                    try
                    {
                        switch (choice)
                        {
                            case "1":
                                Console.Write("Сумма: ");
                                acc.Deposit(decimal.Parse(Console.ReadLine() ?? "0"));
                                break;
                            case "2":
                                Console.Write("Сумма: ");
                                acc.Withdraw(decimal.Parse(Console.ReadLine() ?? "0"));
                                break;
                            case "3":
                                acc.Status = acc.Status == AccountStatus.Active ? AccountStatus.Frozen : AccountStatus.Active;
                                Console.WriteLine($"Статус изменен на: {acc.Status}");
                                break;
                            case "4":
                                Console.WriteLine(acc.GetAccountInfo());
                                break;
                            case "0":
                                running = false;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ОШИБКА: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Критическая ошибка: {ex.Message}");
            }
        }
    }
}