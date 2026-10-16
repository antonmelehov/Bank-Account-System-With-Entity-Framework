using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankSystem.EF.Entities;

[Table("account_owner")]
public class AccountOwner
{
    [Key]
    [Column("account_owner_id")]
    public int Id { get; set; }

    [Column("first_name")]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    public string LastName { get; set; } = null!;

    [Column("email")]
    public string Email { get; set; } = null!;

    public IList<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
}
