using System.Globalization;
using System.Net.Mail;

namespace BankSystem.Services.Helpers;

public static class ValidationService
{
    public static bool IsCurrencyValid(this string currencyCode)
    {
        if (string.IsNullOrWhiteSpace(currencyCode))
        {
            return false;
        }

        return CultureInfo
            .GetCultures(CultureTypes.SpecificCultures)
            .Select(culture => new RegionInfo(culture.Name))
            .Any(region => string.Equals(
                region.ISOCurrencySymbol,
                currencyCode,
                StringComparison.OrdinalIgnoreCase));
    }

    public static bool IsEmailValid(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        return MailAddress.TryCreate(email, out MailAddress? mailAddress)
               && mailAddress.Address == email;
    }
}
