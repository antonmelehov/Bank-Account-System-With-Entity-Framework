using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;

public class GuidNumberGenerator : IUniqueNumberGenerator
{
    public string Generate()
    {
        string guid = Guid.NewGuid().ToString();

        return CryptoHelper.GenerateHash(guid);
    }
}
