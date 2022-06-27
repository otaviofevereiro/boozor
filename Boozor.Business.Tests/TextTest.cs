using Xunit;

namespace Boozor.Business.Tests
{
    public class TextTest
    {
        [Fact]
        public void Text_IsRequired_ExpectValidationRequired()
        {
            Text text = new();

            text.IsRequired();

            Assert.
        }
    }
}