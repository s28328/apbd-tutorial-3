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
        [Obsolete]
        public UserService()
        {
            _clientRepository = new ClientRepository();
            _creditLimitService = new UserCreditService();
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            //Logika biznesowa - walidacja
            if (!Validator.FullClientDataValidation(firstName, lastName, email, dateOfBirth))
                return false;
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
            switch (client.Type)
            {
                case "VeryImportantClient":
                {
                    user.HasCreditLimit = false;
                    break;
                }
                
                case "ImportantClient":
                {
                    int creditLimit = _creditLimitService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;   
                    break;
                }
                    
                default:
                {
                    user.HasCreditLimit = true;
                    int creditLimit = _creditLimitService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                    break;
                }
            }
            _creditLimitService.Dispose();
            //Logika biznesowa - walidacja
            if (Validator.UserCreditValidation(user))
                return false;
            //Infrastruktura
            UserDataAccess.AddUser(user);
            return true;
        }
    }

    public interface IClientRepository
    {
        Client GetById(int clientId);
    }

    public interface ICreditLimitService: IDisposable
    {
        int GetCreditLimit(string lastName, DateTime dateOfBirth);
    }
}
