using LegacyApp;

namespace LegacyAppTests;

public class FakeClientRepository:IClientRepository
{
    public Client GetById(int clientId)
    {
        string type = clientId switch
        {
            1 => "Normal",
            2=> "ImportantClient",
            3=> "VeryImportantClient",
            _=> "None"
        };
        return new Client(type);
    }
}