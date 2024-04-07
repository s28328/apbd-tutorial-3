namespace LegacyAppTests;

using LegacyApp;

public class FakeCreditServiceLimitHigher500:ICreditLimitService
{
    public int GetCreditLimit(string lastName, DateTime dateOfBirth)
    {
        return 600;
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }
}