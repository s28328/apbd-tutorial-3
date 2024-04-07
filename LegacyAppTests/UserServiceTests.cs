using LegacyApp;

namespace LegacyAppTests;

public class UserServiceTests
{
    [Fact]
    [Obsolete("Obsolete")]
    public void AddUser_Should_Return_False_When_Email_Without_At_and_Dot()
    {
        //Arrange
        string firstName = "John";
        string lastName = "Doe";
        DateTime birtDate = new DateTime(1980, 1, 1);
        int clientId = 1;
        string email = "doe";
        IClientRepository clientRepository = new FakeClientRepository();
        ICreditLimitService creditLimitService = new FakeCreditServiceLimitLower500();
        var service = new UserService(clientRepository,creditLimitService);

        //Act
        bool result = service.AddUser(firstName, lastName, email, birtDate, clientId);

        //Assert
        Assert.Equal(false,result);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Missing_FirstName()
    {
        //Arrange
        string firstName = "";
        string lastName = "Doe";
        DateTime birtDate = new DateTime(1980, 1, 1);
        int clientId = 1;
        string email = "doe@gmail.com";
        IClientRepository clientRepository = new FakeClientRepository();
        ICreditLimitService creditLimitService = new FakeCreditServiceLimitLower500();
        var service = new UserService(clientRepository,creditLimitService);

        //Act
        bool result = service.AddUser(firstName, lastName, email, birtDate, clientId);

        //Assert
        Assert.Equal(false,result);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Normal_Client_Has_Limit_Lower_500()
    {
        //Arrange
        string firstName = "John";
        string lastName = "Doe";
        DateTime birtDate = new DateTime(1980, 1, 1);
        int clientId = 1;
        string email = "doe@gmail.com";
        IClientRepository clientRepository = new FakeClientRepository();
        ICreditLimitService creditLimitService = new FakeCreditServiceLimitLower500();
        var service = new UserService(clientRepository,creditLimitService);

        //Act
        bool result = service.AddUser(firstName, lastName, email, birtDate, clientId);

        //Assert
        Assert.Equal(false,result);
        
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Younger_Then_21_Years_Old()
    {
        //Arrange
        string firstName = "";
        string lastName = "Doe";
        DateTime birtDate = new DateTime(2007, 1, 1);
        int clientId = 1;
        string email = "doe@gmail.com";
        IClientRepository clientRepository = new FakeClientRepository();
        ICreditLimitService creditLimitService = new FakeCreditServiceLimitLower500();
        var service = new UserService(clientRepository,creditLimitService);

        //Act
        bool result = service.AddUser(firstName, lastName, email, birtDate, clientId);

        //Assert
        Assert.Equal(false,result);
    }

    [Fact]
    public void AddUser_Should_Return_True_When_Important_Client()
    {
        //Arrange
        string firstName = "John";
        string lastName = "Doe";
        DateTime birtDate = new DateTime(1980, 1, 1);
        int clientId = 2;
        string email = "doe@gmail.com";
        IClientRepository clientRepository = new FakeClientRepository();
        ICreditLimitService creditLimitService = new FakeCreditServiceLimitLower500();
        var service = new UserService(clientRepository,creditLimitService);

        //Act
        bool result = service.AddUser(firstName, lastName, email, birtDate, clientId);

        //Assert
        Assert.Equal(true,result);
    }

    [Fact]
    public void AddUser_Should_Return_True_When_Normal_Client()
    {
        //Arrange
        string firstName = "John";
        string lastName = "Doe";
        DateTime birtDate = new DateTime(1980, 1, 1);
        int clientId = 1;
        string email = "doe@gmail.com";
        IClientRepository clientRepository = new FakeClientRepository();
        ICreditLimitService creditLimitService = new FakeCreditServiceLimitHigher500();
        var service = new UserService(clientRepository,creditLimitService);

        //Act
        bool result = service.AddUser(firstName, lastName, email, birtDate, clientId);

        //Assert
        Assert.Equal(true,result);
    }

    [Fact]
    public void AddUser_Return_True_When_Very_Important_Client()
    {
        //Arrange
        string firstName = "John";
        string lastName = "Doe";
        DateTime birtDate = new DateTime(1980, 1, 1);
        int clientId = 3;
        string email = "doe@gmail.com";
        IClientRepository clientRepository = new FakeClientRepository();
        ICreditLimitService creditLimitService = new FakeCreditServiceLimitLower500();
        var service = new UserService(clientRepository,creditLimitService);

        //Act
        bool result = service.AddUser(firstName, lastName, email, birtDate, clientId);

        //Assert
        Assert.Equal(true,result);
    }

    [Fact]
    public void AddUser_Should_Throw_Exception_When_User_Does_Not_Exist()
    {
        //Arrange
        string firstName = "Daniil";
        string lastName = "Novohurskyi";
        DateTime birtDate = new DateTime(1980, 1, 1);
        int clientId = 10;
        string email = "doe@gmail.com";
        var service = new UserService();
        
        //Assert
        Assert.Throws<ArgumentException>(()=>service.AddUser(firstName, lastName, email, birtDate, clientId));
    }

    [Fact]
    public void AddUser_Should_Throw_Exception_When_User_No_Credit_Limit_Exists_For_User()
    {
        //Arrange
        string firstName = "Daniil";
        string lastName = "Andrzejewicz";
        DateTime birtDate = new DateTime(1980, 1, 1);
        int clientId = 1;
        string email = "doe@gmail.com";
        var service = new UserService();
        
        //Assert
        Assert.Throws<ArgumentException>(()=>service.AddUser(firstName, lastName, email, birtDate, clientId));
  
    }
}