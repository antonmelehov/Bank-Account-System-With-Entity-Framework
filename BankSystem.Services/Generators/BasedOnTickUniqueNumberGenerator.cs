using System.Globalization;
using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;

public class BasedOnTickUniqueNumberGenerator : IUniqueNumberGenerator
{
    private readonly DateTime startingPoint;

    public BasedOnTickUniqueNumberGenerator(DateTime startingPoint)
    {
        this.startingPoint = startingPoint;
    }

    public string Generate()
    {
        long elapsedTicks = (DateTime.Now - this.startingPoint).Ticks;

        return CryptoHelper.GenerateHash(elapsedTicks.ToString(CultureInfo.InvariantCulture));
    }
}
