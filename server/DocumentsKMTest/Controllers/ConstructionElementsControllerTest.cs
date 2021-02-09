// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Net;
// using System.Net.Http;
// using System.Text.Json;
// using System.Threading.Tasks;
// using DocumentsKM.Dtos;
// using FluentAssertions;
// using Microsoft.AspNetCore.Authorization.Policy;
// using Microsoft.AspNetCore.TestHost;
// using Microsoft.Extensions.DependencyInjection;
// using Xunit;

// namespace DocumentsKM.Tests
// {
//     // TBD: Create, Update, Delete
//     public class ConstructionElementsControllerTest : IClassFixture<TestWebApplicationFactory<DocumentsKM.Startup>>
//     {
//         private readonly HttpClient _authHttpClient;
//         private readonly HttpClient _httpClient;
//         private readonly Random _rnd = new Random();

//         public ConstructionElementsControllerTest(TestWebApplicationFactory<DocumentsKM.Startup> factory)
//         {
//             _httpClient = factory.WithWebHostBuilder(builder =>
//             {
//                 builder.ConfigureTestServices(services =>
//                 {
//                     services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
//                 });
//             }).CreateClient();
            
//             _authHttpClient = factory.CreateClient();
//         }

//         [Fact]
//         public async Task GetAllByConstructionId_ShouldReturnOK_WhenAccessTokenIsProvided()
//         {
//             // Arrange
//             int constructionId = _rnd.Next(1, TestData.constructions.Count());
//             var endpoint = $"/api/constructions/{constructionId}/elements";

//             // Act
//             var response = await _httpClient.GetAsync(endpoint);

//             // Assert
//             Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//             string responseBody = await response.Content.ReadAsStringAsync();

//             var constructionElements = TestData.constructionElements.Where(
//                 v => v.Construction.Id == constructionId)
//                 .Select(v => new ConstructionElementResponse
//                 {
//                     Id = v.Id,
//                     ProfileClass = v.ProfileClass,
//                     Profile = v.Profile,
//                     Steel = v.Steel,
//                     Length = v.Length,
//                 }).ToArray();
//             var options = new JsonSerializerOptions()
//             {
//                 PropertyNamingPolicy = JsonNamingPolicy.CamelCase
//             };
//             JsonSerializer.Deserialize<IEnumerable<ConstructionElementResponse>>(
//                 responseBody, options).Should().BeEquivalentTo(constructionElements);
//         }

//         [Fact]
//         public async Task GetAllByConstructionId_ShouldReturnUnauthorized_WhenNoAccessToken()
//         {
//             // Arrange
//             int constructionId = _rnd.Next(1, TestData.constructions.Count());
//             var endpoint = $"/api/constructions/{constructionId}/elements";

//             // Act
//             var response = await _authHttpClient.GetAsync(endpoint);

//             // Assert
//             Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
//         }
//     }
// }
