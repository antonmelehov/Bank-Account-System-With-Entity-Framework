using System.Globalization;
using BankSystem.Services.Generators;
using BankSystem.Services.Helpers;

namespace BankSystem.Services.Models.Accounts;

public abstract class BankAccount
{
    private readonly List<AccountCashOperation> operations = new List<AccountCashOperation>();

    protected BankAccount(
        AccountOwner owner,
        string currencyCode,
        IUniqueNumberGenerator uniqueNumberGenerator)
    {
        ArgumentNullException.ThrowIfNull(owner);

        ArgumentNullException.ThrowIfNull(uniqueNumberGenerator);

        if (!currencyCode.IsCurrencyValid())
        {
            throw new ArgumentException("Currency code is invalid.", nameof(currencyCode));
        }

        this.AccountOwner = owner;
        this.CurrencyCode = currencyCode;
        this.Number = uniqueNumberGenerator.Generate();

        owner.Add(this);
    }

    protected BankAccount(
        AccountOwner owner,
        string currencyCode,
        Func<string> numberGenerator)
    {
        ArgumentNullException.ThrowIfNull(owner);

        ArgumentNullException.ThrowIfNull(numberGenerator);

        if (!currencyCode.IsCurrencyValid())
        {
            throw new ArgumentException("Currency code is invalid.", nameof(currencyCode));
        }

        this.AccountOwner = owner;
        this.CurrencyCode = currencyCode;
        this.Number = numberGenerator();

        owner.Add(this);
    }

    protected BankAccount(
        AccountOwner owner,
        string currencyCode,
        IUniqueNumberGenerator uniqueNumberGenerator,
        decimal initialBalance)
        : this(owner, currencyCode, uniqueNumberGenerator)
    {
        this.Deposit(initialBalance, DateTime.Now, "Initial deposit");
    }

    protected BankAccount(
        AccountOwner owner,
        string currencyCode,
        Func<string> numberGenerator,
        decimal initialBalance)
        : this(owner, currencyCode, numberGenerator)
    {
        this.Deposit(initialBalance, DateTime.Now, "Initial deposit");
    }

    public string Number { get; private set; }

    public decimal Balance { get; private set; }

    public string CurrencyCode { get; private set; }

    public AccountOwner AccountOwner { get; private set; }

    public int BonusPoints { get; private set; }

    protected abstract decimal Overdraft { get; }

    public void Deposit(decimal amount, DateTime date, string note)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        this.Balance += amount;

        this.operations.Add(
            new AccountCashOperation(amount, date, note));

        this.BonusPoints += this.CalculateDepositRewardPoints(amount);
    }

    public void Withdraw(decimal amount, DateTime date, string note)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        if (amount > this.Balance + this.Overdraft)
        {
            throw new InvalidOperationException();
        }

        this.Balance -= amount;

        this.operations.Add(
            new AccountCashOperation(-amount, date, note));

        this.BonusPoints += this.CalculateWithdrawRewardPoints(amount);
    }

    public IList<AccountCashOperation> GetAllOperations()
    {
        return this.operations;
    }

    public override string ToString()
    {
        return string.Format(
            CultureInfo.InvariantCulture,
            "{0} {1} No:{2}. Balance: {3}{4}.\n",
            this.Overdraft,
            this.AccountOwner,
            this.Number,
            this.Balance,
            this.CurrencyCode);
    }

    protected abstract int CalculateDepositRewardPoints(decimal amount);

    protected abstract int CalculateWithdrawRewardPoints(decimal amount);
}
