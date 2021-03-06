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
    public class MarkApprovalsControllerTest : IClassFixture<TestWebApplicationFactory<DocumentsKM.Startup>>
    {
        private readonly HttpClient _authHttpClient;
        private readonly HttpClient _httpClient;
        private readonly Random _rnd = new Random();

        public MarkApprovalsControllerTest(TestWebApplicationFactory<DocumentsKM.Startup> factory)
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
        public async Task GetAllByMarkId_ShouldReturnOK()
        {
            // Arrange
            int markId = _rnd.Next(1, TestData.marks.Count());
            var endpoint = $"/api/marks/{markId}/approvals";

            // Act
            var response = await _httpClient.GetAsync(endpoint);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            string responseBody = await response.Content.ReadAsStringAsync();

            var employees = TestData.markApprovals.Where(v => v.Mark.Id == markId)
                .Select(v => new EmployeeDepartmentResponse
                {
                    Id = v.Employee.Id,
                    Name = v.Employee.Name,
                    Department = v.Employee.Department,
                }).ToArray();
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            JsonSerializer.Deserialize<IEnumerable<EmployeeDepartmentResponse>>(
                responseBody, options).Should().BeEquivalentTo(employees);
        }

        [Fact]
        public async Task GetAllByMarkId_ShouldReturnUnauthorized_WhenNoAccessToken()
        {
            // Arrange
            int markId = _rnd.Next(1, TestData.marks.Count());
            var endpoint = $"/api/marks/{markId}/approvals";

            // Act
            var response = await _authHttpClient.GetAsync(endpoint);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}