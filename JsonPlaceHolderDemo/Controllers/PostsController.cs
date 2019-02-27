using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JsonPlaceHolderDemo.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JsonPlaceHolderDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var httpClient = new HttpClient();

            var httpResponse = await httpClient.GetAsync(@"https://jsonplaceholder.typicode.com/posts").ConfigureAwait(false);
            if (!httpResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, httpResponse.ReasonPhrase);
            }

            var getPostsResponse = JsonConvert.DeserializeObject<List<GetPostResponse>>(await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false));

            return Ok(getPostsResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var httpClient = new HttpClient();

            var httpResponse = await httpClient.GetAsync($@"http://jsonplaceholder.typicode.com/posts/{id}").ConfigureAwait(false);
            if (!httpResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, httpResponse.ReasonPhrase);
            }

            var getPostResponse = JsonConvert.DeserializeObject<GetPostResponse>(await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false));

            if (getPostResponse == null)
            {
                return NotFound();
            }

            return Ok(getPostResponse);
        }
    }
}