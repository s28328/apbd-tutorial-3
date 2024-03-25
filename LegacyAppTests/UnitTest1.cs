using LegacyApp;

namespace LegacyAppTests;

public class UnitTest1
{
    [Fact]
    public void AddUser_Should_Return_False_When_Email_Without_At_and_Dot()
    {
        //Arrange
        string firstName = "John";
        string lastName = "Doe";
        DateTime birtDate = new DateTime(1980, 1, 1);
        int clientId = 1;
        string email = "doe";
        var service = new UserService();

        //Act
        bool result = service.AddUser(firstName, lastName, email, birtDate, clientId);

        //Assert
        Assert.Equal(false,result);
    }
}