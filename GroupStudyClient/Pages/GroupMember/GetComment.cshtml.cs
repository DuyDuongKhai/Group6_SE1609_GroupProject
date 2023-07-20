using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GroupStudyClient.Pages.GroupMember
{
    public class GetCommentModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public GetCommentModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public List<Comment> Comments { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var apiUrl = "https://localhost:44340/api/GroupMember/GetAllComments";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Comments = JsonConvert.DeserializeObject<List<Comment>>(content);
            }
            else
            {
                Comments = new List<Comment>();
            }

            return Page();
        }
    }
}
