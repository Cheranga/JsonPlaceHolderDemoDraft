using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JsonPlaceHolderDemo.DTO.Responses;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using Xunit;

namespace JsonPlaceHolderDemo.IntegrationTests
{
    public class PostsControllerTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        public PostsControllerTests(TestWebApplicationFactory testWebApplicationFactory)
        {
            _httpClient = testWebApplicationFactory.CreateClient();
        }


        [Fact]
        public async Task There_Must_Be_Posts()
        {
            var httpResponse = await _httpClient.GetAsync(@"/api/posts");
            httpResponse.EnsureSuccessStatusCode();

            var getPostsResponse = JsonConvert.DeserializeObject<List<GetPostResponse>>(await httpResponse.Content.ReadAsStringAsync());
            Assert.NotNull(getPostsResponse);
            Assert.True(getPostsResponse.Any());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task For_Valid_PostId_The_Post_Data_Must_Be_Available(int id)
        {
            var httpResponse = await _httpClient.GetAsync($@"/api/posts/{id}");
            httpResponse.EnsureSuccessStatusCode();

            var getPostResponse = JsonConvert.DeserializeObject<GetPostResponse>(await httpResponse.Content.ReadAsStringAsync());
            Assert.NotNull(getPostResponse);
            Assert.Equal(id, getPostResponse.Id);
        }

    }
}