using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using StoresApi.Models;
using StoresApi.Repositories;
using System.Net;
using System.Text;
using System.Text.Json;

namespace StoresAPI.Test
{
    public class StoreApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly IStoreRepository _storeRepository;

        public StoreApiTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _storeRepository = _factory.Services.GetRequiredService<IStoreRepository>();
        }

        private int AddStoreAndRetriveId(Store store)
        {
            var existingStore = _storeRepository.Add(store);
            var existingStoreId = existingStore.Id;
            return existingStoreId;
        }

        [Fact]
        public async Task CreateStore_ValidData_ShouldSucceed()
        {
            // Arrange
            var client = _factory.CreateClient();
            var storeData = new Store() { Name = "Test Store", CompanyId = 1, Location = "Brazil" };
            int companyId = 1;
            var storeSerialized = JsonSerializer.Serialize(storeData);

            // Act
            var response = await client.PostAsync($"/company/{companyId}/stores",
                new StringContent(storeSerialized, Encoding.UTF8, "application/json"));
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var serializedResponse = JsonSerializer.Deserialize<Store>(await response.Content.ReadAsStringAsync(), options);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(storeData.Name, serializedResponse.Name);
            Assert.Equal(storeData.Location, serializedResponse.Location);
        }

        [Fact]
        public async Task CreateStore_InvalidData_ShouldFail()
        {
            // Arrange
            var client = _factory.CreateClient();
            var storeData = new { CompanyId = 1 };
            int companyId = 1;
            var storeSerialized = JsonSerializer.Serialize(storeData);

            // Act
            var response = await client.PostAsync($"/company/{companyId}/stores",
                new StringContent(storeSerialized, Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ReadStore_ExistingStoreId_ShouldReturnStore()
        {
            // Arrange
            var client = _factory.CreateClient();
            var store = new Store() { Name = "ExistingStore", CompanyId = 1, Location = "Brazil" };
            int existingStoreId = AddStoreAndRetriveId(store);
            int companyId = 1;

            // Act
            var response = await client.GetAsync($"/company/{companyId}/stores/{existingStoreId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ReadStore_NonExistingId_ShouldReturnNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();
            int id = 789;
            int companyId = 1;

            // Act
            var response = await client.GetAsync($"/company/{companyId}/stores/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateStore_ExistingStoreId_ValidData_ShouldSucceed()
        {
            // Arrange
            var client = _factory.CreateClient();
            var store = new Store() { Name = "ExistingStoreToBeUpdated", CompanyId = 1, Location = "Brazil" };
            int existingStoreId = AddStoreAndRetriveId(store);
            int companyId = 1;
            var updatedStoreData = new { Name = "Updated Store Name" };

            // Act
            var response = await client.PutAsync($"/company/{companyId}/stores/{existingStoreId}",
                new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(updatedStoreData), Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteStore_ExistingStoreId_ShouldSucceed()
        {
            // Arrange
            var client = _factory.CreateClient();
            var store = new Store() { Name = "ExistingStoreToBeDelete", CompanyId = 1, Location = "Brazil" };
            int existingStoreId = AddStoreAndRetriveId(store);
            int companyId = 1;

            // Act
            var response = await client.DeleteAsync($"/company/{companyId}/stores/{existingStoreId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}