using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GroupStudyClient.Pages.GroupMember
{
    public class GetCommentByIdModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public GetCommentByIdModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public List<Comment> Comments { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var apiUrl = $"https://localhost:5001/api/GroupMember/GetCommentsByPostId/{id}";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Comments = JsonConvert.DeserializeObject<List<Comment>>(content);
            }
            else
            {
                return NotFound();
            }

            return Page();
        }
    }
}
