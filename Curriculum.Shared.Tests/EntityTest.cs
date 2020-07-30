using System;
using Xunit;

namespace Curriculum.Shared.Tests
{
    public class EntityTest
    {
        [Fact]
        public void Clone_ValidEntity_ExpectedCopyFieldsInNewInstance()
        {
            var person = new Person()
            { 
                Name = "teste",
                Email= "teste@teste.com.br"
            };


            var personClone = person.Clone();

            Assert.NotNull(personClone);
            Assert.NotEqual(personClone, person);
            Assert.Equal(person.Name, personClone.Name);
            Assert.Equal(person.Email, personClone.Email);
        }
    }
}
