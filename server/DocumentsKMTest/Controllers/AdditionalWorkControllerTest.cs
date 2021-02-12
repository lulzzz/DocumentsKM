using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DocumentsKM.Dtos;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DocumentsKM.Tests
{
    public class AdditionalWorkControllerTest : IClassFixture<TestWebApplicationFactory<DocumentsKM.Startup>>
    {
        private readonly HttpClient _authHttpClient;
        private readonly HttpClient _httpClient;
        private readonly Random _rnd = new Random();

        public AdditionalWorkControllerTest(TestWebApplicationFactory<DocumentsKM.Startup> factory)
        {
            _httpClient = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                });
            }).CreateClient();
            
            _authHttpClient = factory.CreateClient();
        }

        // ------------------------------------GET------------------------------------

        [Fact]
        public async Task GetAllByMarkId_ShouldReturnOK_WhenAccessTokenIsProvided()
        {
            // Arrange
            int markId = _rnd.Next(1, TestData.marks.Count());
            var endpoint = $"/api/marks/{markId}/additional-work";

            // Act
            var response = await _httpClient.GetAsync(endpoint);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllByMarkId_ShouldReturnUnauthorized_WhenNoAccessToken()
        {
            // Arrange
            int markId = _rnd.Next(1, TestData.marks.Count());
            var endpoint = $"/api/marks/{markId}/additional-work";

            // Act
            var response = await _authHttpClient.GetAsync(endpoint);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        // ------------------------------------POST------------------------------------

        [Fact]
        public async Task Create_ShouldReturnCreated()
        {
            // Arrange
            int markId = 1;
            int employeeId = 5;
            var additionalWorkRequest = new AdditionalWorkCreateRequest
            {
                EmployeeId = employeeId,
                Valuation = 9,
                MetalOrder = 9,
            };
            string json = JsonSerializer.Serialize(additionalWorkRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var endpoint = $"/api/marks/{markId}/additional-work";

            // Act
            var response = await _httpClient.PostAsync(endpoint, httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenWrongValues()
        {
            // Arrange
            int markId = 1;
            int employeeId = 4;
            var wrongAdditionalWorkRequests = new List<AdditionalWorkCreateRequest>
            {
                new AdditionalWorkCreateRequest
                {
                    EmployeeId = employeeId,
                    Valuation = -9,
                    MetalOrder = 9,
                },
                new AdditionalWorkCreateRequest
                {
                    EmployeeId = employeeId,
                    Valuation = 9,
                    MetalOrder = -9,
                },
            };

            var endpoint = $"/api/marks/{markId}/additional-work";
            foreach (var wrongAdditionalWorkRequest in wrongAdditionalWorkRequests)
            {
                var json = JsonSerializer.Serialize(wrongAdditionalWorkRequest);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                // Act
                var response = await _httpClient.PostAsync(endpoint, httpContent);

                // Assert
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task Create_ShouldReturnNotFound_WhenWrongValues()
        {
            // Arrange
            int markId = 1;
            int employeeId = 4;
            var additionalWorkRequest = new AdditionalWorkCreateRequest
            {
                EmployeeId = employeeId,
                Valuation = 9,
                MetalOrder = 9,
            };
            var wrongAdditionalWorkRequest = new AdditionalWorkCreateRequest
            {
                EmployeeId = 999,
                Valuation = 9,
                MetalOrder = 9,
            };
            string json1 = JsonSerializer.Serialize(wrongAdditionalWorkRequest);
            string json2 = JsonSerializer.Serialize(additionalWorkRequest);
            var httpContent1 = new StringContent(json1, Encoding.UTF8, "application/json");
            var httpContent2 = new StringContent(json2, Encoding.UTF8, "application/json");
            var endpoint1 = $"/api/marks/{markId}/additional-work";
            var endpoint2 = $"/api/marks/{999}/additional-work";

            // Act
            var response1 = await _httpClient.PostAsync(endpoint1, httpContent1);
            var response2 = await _httpClient.PostAsync(endpoint2, httpContent2);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response1.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response2.StatusCode);
        }

        [Fact]
        public async Task Create_ShouldReturnConflict_WhenConflictValues()
        {
            // Arrange
            int markId = 1;
            int employeeId = 1;
            var additionalWorkRequest = new AdditionalWorkCreateRequest
            {
                EmployeeId = employeeId,
                Valuation = 9,
                MetalOrder = 9,
            };
            string json = JsonSerializer.Serialize(additionalWorkRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var endpoint = $"/api/marks/{markId}/additional-work";

            // Act
            var response = await _httpClient.PostAsync(endpoint, httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Fact]
        public async Task Create_ShouldReturnUnauthorized_WhenNoAccessToken()
        {
            // Arrange
            int markId = 1;
            int employeeId = 4;
            var additionalWorkRequest = new AdditionalWorkCreateRequest
            {
                EmployeeId = employeeId,
                Valuation = 9,
                MetalOrder = 9,
            };
            string json = JsonSerializer.Serialize(additionalWorkRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var endpoint = $"/api/marks/{markId}/additional-work";

            // Act
            var response = await _authHttpClient.PostAsync(endpoint, httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        // ------------------------------------PATCH------------------------------------

        [Fact]
        public async Task Update_ShouldReturnNoContent()
        {
            // Arrange
            int id = 1;
            var additionalWorkRequest = new AdditionalWorkUpdateRequest
            {
                Valuation = 9,
                MetalOrder = 9,
            };
            string json = JsonSerializer.Serialize(additionalWorkRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var endpoint = $"/api/additional-work/{id}";

            // Act
            var response = await _httpClient.PatchAsync(endpoint, httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenWrongValues()
        {
            // Arrange
            int id = 1;
            var wrongAdditionalWorkRequests = new List<AdditionalWorkUpdateRequest>
            {
                new AdditionalWorkUpdateRequest
                {
                    Valuation = -9,
                    MetalOrder = 9,
                },
                new AdditionalWorkUpdateRequest
                {
                    Valuation = 9,
                    MetalOrder = -9,
                },
            };

            var endpoint = $"/api/additional-work/{id}";
            foreach (var wrongAdditionalWorkRequest in wrongAdditionalWorkRequests)
            {
                var json = JsonSerializer.Serialize(wrongAdditionalWorkRequest);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                // Act
                var response = await _httpClient.PatchAsync(endpoint, httpContent);

                // Assert
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenWrongValues()
        {
            // Arrange
            int id = 1;
            var additionalWorkRequest = new AdditionalWorkUpdateRequest
            {
                Valuation = 9,
                MetalOrder = 9,
            };
            var wrongAdditionalWorkRequest = new AdditionalWorkUpdateRequest
            {
                EmployeeId = 999,
            };
            string json1 = JsonSerializer.Serialize(wrongAdditionalWorkRequest);
            string json2 = JsonSerializer.Serialize(additionalWorkRequest);
            var httpContent1 = new StringContent(json1, Encoding.UTF8, "application/json");
            var httpContent2 = new StringContent(json2, Encoding.UTF8, "application/json");
            var endpoint1 = $"/api/additional-work/{id}";
            var endpoint2 = $"/api/additional-work/{999}";

            // Act
            var response1 = await _httpClient.PatchAsync(endpoint1, httpContent1);
            var response2 = await _httpClient.PatchAsync(endpoint2, httpContent2);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response1.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response2.StatusCode);
        }

        [Fact]
        public async Task Update_ShouldReturnConflict_WhenConflictValues()
        {
            // Arrange
            int id = 3;
            var additionalWorkRequest = new AdditionalWorkUpdateRequest
            {
                EmployeeId = 2,
                Valuation = 9,
                MetalOrder = 9,
            };
            string json = JsonSerializer.Serialize(additionalWorkRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var endpoint = $"/api/additional-work/{id}";

            // Act
            var response = await _httpClient.PatchAsync(endpoint, httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Fact]
        public async Task Update_ShouldReturnUnauthorized_WhenNoAccessToken()
        {
            // Arrange
            int id = 1;
            var additionalWorkRequest = new AdditionalWorkUpdateRequest
            {
                Valuation = 9,
                MetalOrder = 9,
            };
            string json = JsonSerializer.Serialize(additionalWorkRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var endpoint = $"/api/additional-work/{id}";

            // Act
            var response = await _authHttpClient.PatchAsync(endpoint, httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        // ------------------------------------DELETE------------------------------------

        [Fact]
        public async Task Delete_ShouldReturnNoContent()
        {
            // Arrange
            int id = 2;
            var endpoint = $"/api/additional-work/{id}";

            // Act
            var response = await _httpClient.DeleteAsync(endpoint);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenWrongId()
        {
            // Arrange
            var endpoint = $"/api/additional-work/{999}";

            // Act
            var response = await _httpClient.DeleteAsync(endpoint);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturnUnauthorized_WhenNoAccessToken()
        {
            // Arrange
            int id = 2;
            var endpoint = $"/api/additional-work/{id}";

            // Act
            var response = await _authHttpClient.DeleteAsync(endpoint);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
