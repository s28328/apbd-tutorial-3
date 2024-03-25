using System;

namespace LegacyApp
{
    public class UserService
    {

        private IClientRepository _clientRepository;
        private ICreditLimitService _creditLimitService;

        public UserService(IClientRepository clientRepository, ICreditLimitService creditLimitService)
        {
            _clientRepository = clientRepository;
            _creditLimitService = creditLimitService;
        }
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            //Logika biznesowa - walidacja
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }
            //Logika biznesowa - walidacja
            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }
            //Logika biznesowa - wiek
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }
            //Infrastruktura
            //var clientRepository = new ClientRepository();
            var client = _clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
            //Logika biznesowa + Infrastruktura - walidacja
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = _creditLimitService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = _creditLimitService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }
            //Logika biznesowa - walidacja
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }
            //Infrastruktura
            UserDataAccess.AddUser(user);
            return true;
        }
    }

    public interface IClientRepository
    {
        Client GetById(int clientId);
    }

    public interface ICreditLimitService
    {
        int GetCreditLimit(string lastName, DateTime dateOfBirth);
    }
}
