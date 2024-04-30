using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;
using System.Text.Json;

namespace StoresAPI.Test
{
    public class StoreApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public StoreApiTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateStore_ValidData_ShouldSucceed()
        {
            // Arrange
            var client = _factory.CreateClient();
            var storeData = new { Name = "Test Store", CompanyId = 1, Location = "Brazil" };
            var storeSerialized = JsonSerializer.Serialize(storeData);

            // Act
            var response = await client.PostAsync("/stores",
                new StringContent(storeSerialized, Encoding.UTF8, "application/json"));
            var serializedResponse = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(storeSerialized, serializedResponse);
        }

        [Fact]
        public async Task CreateStore_InvalidData_ShouldFail()
        {
            // Arrange
            var client = _factory.CreateClient();
            var storeData = new { CompanyId = 1 };
            var storeSerialized = JsonSerializer.Serialize(storeData);

            // Act
            var response = await client.PostAsync("/stores",
                new StringContent(storeSerialized, Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ReadStore_ExistingStoreId_ShouldReturnStore()
        {
            // Arrange
            var client = _factory.CreateClient();
            int existingStoreId = 123;

            // Act
            var response = await client.GetAsync($"/stores/{existingStoreId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ReadStore_NonExistingId_ShouldReturnNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();
            int id = 789;

            // Act
            var response = await client.GetAsync($"/stores/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateStore_ExistingStoreId_ValidData_ShouldSucceed()
        {
            // Arrange
            var client = _factory.CreateClient();
            int existingStoreId = 123;
            var updatedStoreData = new { Name = "Updated Store Name" };

            // Act
            var response = await client.PutAsync($"/stores/{existingStoreId}",
                new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(updatedStoreData), Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteStore_ExistingStoreId_ShouldSucceed()
        {
            // Arrange
            var client = _factory.CreateClient();
            int existingStoreId = 123;

            // Act
            var response = await client.DeleteAsync($"/stores/{existingStoreId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}