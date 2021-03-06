using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DocumentsKM.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DocumentsKM.Tests
{
    // TBD: Create, Update, Delete
    public class GeneralDataPointsControllerTest : IClassFixture<TestWebApplicationFactory<DocumentsKM.Startup>>
    {
        private readonly HttpClient _authHttpClient;
        private readonly HttpClient _httpClient;
        private readonly Random _rnd = new Random();

        private readonly int _maxUserId = 3;
        private readonly int _maxSectionId = 3;

        public GeneralDataPointsControllerTest(TestWebApplicationFactory<DocumentsKM.Startup> factory)
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

        [Fact]
        public async Task GetAllByUserId_ShouldReturnOK()
        {
            // Arrange
            int userId = _rnd.Next(1, _maxUserId);
            int sectionId = _rnd.Next(1, _maxSectionId);
            var endpoint = $"/api/users/{userId}/general-data-sections/{sectionId}/general-data-points";

            // Act
            var response = await _httpClient.GetAsync(endpoint);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            string responseBody = await response.Content.ReadAsStringAsync();

            var GeneralDataPoints = TestData.generalDataPoints.Where(
                v => v.User.Id == userId && v.Section.Id == sectionId)
                .Select(v => new GeneralDataPointResponse
                {
                    Id = v.Id,
                    Text = v.Text,
                    OrderNum = v.OrderNum,
                }).ToArray();
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            JsonSerializer.Deserialize<IEnumerable<GeneralDataPointResponse>>(
                responseBody, options).Should().BeEquivalentTo(GeneralDataPoints);
        }

        [Fact]
        public async Task GetAllByUserId_ShouldReturnUnauthorized_WhenNoAccessToken()
        {
            // Arrange
            int userId = _rnd.Next(1, _maxUserId);
            int sectionId = _rnd.Next(1, _maxSectionId);
            var endpoint = $"/api/users/{userId}/general-data-sections/{sectionId}/general-data-points";

            // Act
            var response = await _authHttpClient.GetAsync(endpoint);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
