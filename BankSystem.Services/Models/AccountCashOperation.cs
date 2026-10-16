using System.Globalization;

namespace BankSystem.Services.Models;

public class AccountCashOperation
{
    public AccountCashOperation(decimal amount, DateTime date, string note)
    {
        this.Amount = amount;
        this.Date = date;
        this.Note = note;
    }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public string Note { get; set; }

    public override string ToString()
    {
        string operationType = this.Amount < 0
            ? "Debited from account"
            : "Credited to account";

        return string.Format(
            CultureInfo.InvariantCulture,
            "{0:MM/dd/yyyy HH:mm:ss} {1} : {2} {3}.",
            this.Date,
            this.Note,
            operationType,
            this.Amount);
    }
}
