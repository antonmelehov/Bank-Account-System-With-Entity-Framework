using BankSystem.Services.Helpers;
using BankSystem.Services.Models.Accounts;

namespace BankSystem.Services.Models;

public class AccountOwner
{
    private string firstName;
    private string lastName;
    private string email;

    private readonly List<BankAccount> accounts = new List<BankAccount>();

    public AccountOwner(string firstName, string lastName, string email)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
    }

    public string FirstName
    {
        get
        {
            return this.firstName;
        }

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("First name cannot be null, empty, or whitespace.");
            }

            this.firstName = value;
        }
    }

    public string LastName
    {
        get
        {
            return this.lastName;
        }

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Last name cannot be null, empty, or whitespace.");
            }

            this.lastName = value;
        }
    }

    public string Email
    {
        get
        {
            return this.email;
        }

        set
        {
            if (!value.IsEmailValid())
            {
                throw new ArgumentException("Email address is invalid.");
            }

            this.email = value;
        }
    }

    public void Add(BankAccount account)
    {
        this.accounts.Add(account);
    }

    public IList<BankAccount> Accounts()
    {
        return this.accounts;
    }

    public override string ToString()
    {
        return $"{this.FirstName} {this.LastName}, {this.Email}.";
    }
}
