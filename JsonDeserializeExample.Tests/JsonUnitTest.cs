using System.Text.Json;
using JsonDeserializeExample.Library.Entity;
using Xunit;

namespace JsonDeserializeExample.Tests
{
    public class JsonUnitTest
    {
        [Fact]
        public void ShouldDeserializeStringIntoPerson()
        {
            var jsonFormat = "{ \"name\": \"Person Name\", \"age\": 18, \"address\": \"Address Example\", \"zipCode\": \"02028-525\" }";

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var person = JsonSerializer.Deserialize<Person>(jsonFormat, jsonOptions);

            Assert.Equal("Person Name", person.Name);
            Assert.Equal(18, person.Age);
            Assert.Equal("Address Example", person.Address);
            Assert.Equal("02028-525", person.ZipCode);
        }
    }
}
