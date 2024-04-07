using System;

namespace LegacyApp;

public static class Validator
{
    private static bool NamesValidation(string firstName, string lastName)
    {
        return string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName);
    }

    private static bool EmailValidation(string email)
    {
        return !email.Contains("@") && !email.Contains(".");
    }

    private static bool AgeValidation(DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        int age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

        return age < 21;
    }

    public static bool FullClientDataValidation(string firstName, string lastName, string email, DateTime dateOfBirth)
    {
        if (Validator.NamesValidation(firstName, lastName))
            return false;
        if (Validator.EmailValidation(email))
            return false;
        if (Validator.AgeValidation(dateOfBirth))
            return false;
        return true;
    }

    public static bool UserCreditValidation(User user)
    {
            return user.HasCreditLimit && user.CreditLimit < 500;
    }
}