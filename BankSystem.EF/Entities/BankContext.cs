using Microsoft.EntityFrameworkCore;

namespace BankSystem.EF.Entities;

public class BankContext : DbContext
{
    public BankContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<AccountCashOperation> AccountCashOperations { get; set; } = null!;

    public DbSet<AccountOwner> AccountOwners { get; set; } = null!;

    public DbSet<BankAccount> BankAccounts { get; set; } = null!;

    public DbSet<CurrencyCode> CurrencyCodes { get; set; } = null!;
}
