using LegacyApp;
namespace LegacyAppTests;

public class FakeCreditServiceLimitLower500:ICreditLimitService
{
    public int GetCreditLimit(string lastName, DateTime dateOfBirth)
    {
        return 300;
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }
}