using System;

namespace BankSystem
{
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