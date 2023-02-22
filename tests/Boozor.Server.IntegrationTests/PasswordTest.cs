using Boozor.Server.Authentication;

namespace Boozor.Server.IntegrationTests;

public class PasswordTest
{
    [Theory]
    [InlineData("teste", "my_custom_password_teste_1234")]
    [InlineData("sdfdsf", "sdfsdfsdf^&&()*@#&¨")]
    public void Encrypt_ExpectedLoginWithSuccess(string user, string password)
    {
        //Arrange
        Login login = new(user, password);

        //Act
        string hash = Password.Encrypt(login);
        bool isLogged = Password.Login(login, hash);

        //Assert
        Assert.True(isLogged);
    }
}
