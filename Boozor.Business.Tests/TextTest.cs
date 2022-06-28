using Xunit;

namespace Boozor.Business.Tests
{
    public class TextTest
    {
        [Fact]
        public void Text_IsRequired_ExpectValidationRequired()
        {
            //Arrange
            Text text = new();

            //Act
            text.IsRequired();
            text.Value = null;

            //Assert
            var result = Assert.Single(text.GetResults());
            Assert.IsType<Error>(result);
            Assert.Equal("Campo obrigatório preenchimento.", result.Message);
        }
    }
}