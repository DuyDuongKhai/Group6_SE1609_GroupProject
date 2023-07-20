using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GroupStudyClient.Pages.GroupMember
{
    public class GetPostModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public GetPostModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Post Post { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var apiUrl = "https://localhost:44340/api/GroupMember/GetAllPosts";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var posts = JsonConvert.DeserializeObject<List<Post>>(content);
                ViewData["Posts"] = posts;
            }
            else
            {
                ViewData["ErrorMessage"] = "Error retrieving posts.";
            }

            return Page();
        }

    }
}
