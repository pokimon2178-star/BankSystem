using System;

namespace BankSystem
{
    public class AccountFrozenError : Exception { public AccountFrozenError(string m) : base(m) { } }
    public class AccountClosedError : Exception { public AccountClosedError(string m) : base(m) { } }
    public class InvalidOperationError : Exception { public InvalidOperationError(string m) : base(m) { } }
    public class InsufficientFundsError : Exception { public InsufficientFundsError(string m) : base(m) { } }
}