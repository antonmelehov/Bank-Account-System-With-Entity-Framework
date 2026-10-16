using System.Globalization;
using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;

public class SimpleGenerator : IUniqueNumberGenerator
{
    private long lastNumber = 1234567890;

    static SimpleGenerator()
    {
        Instance = new SimpleGenerator();
    }

    private SimpleGenerator()
    {
    }

    public static SimpleGenerator Instance { get; }

    public string Generate()
    {
        this.lastNumber++;

        return CryptoHelper.GenerateHash(this.lastNumber.ToString(CultureInfo.InvariantCulture));
    }
}
