using System;
using System.IO;
using System.Text.Json;
using JsonDeserializeExample.Library.DTO;
using Xunit;

namespace JsonDeserializeExample.Tests
{
    public class JsonSerializerTest
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

        [Fact]
        public void ShouldDeserializeFileContentIntoPerson()
        {
            var codeBaseUrl = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var compiledDirectoryPath = Path.GetDirectoryName(codeBasePath);
            var jsonFile = Path.Combine(string.Join('\\', compiledDirectoryPath.Split('\\')[..^3]), @"Support\person.json");
            
            using (var fileStream = new StreamReader(jsonFile))
            {
                byte[] fileBuffer = new byte[fileStream.BaseStream.Length];
                fileStream.BaseStream.Read(fileBuffer, 0, fileBuffer.Length);

                var reader = new Utf8JsonReader(fileBuffer);
                var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var person = JsonSerializer.Deserialize<Person>(ref reader, jsonOptions);

                Assert.Equal("Person Name", person.Name);
                Assert.Equal(18, person.Age);
                Assert.Equal("Address Example", person.Address);
                Assert.Equal("02028-525", person.ZipCode);
            }
        }
    }
}
