using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObject.Models;
using DataAccess;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;



namespace GroupStudyClient.Pages.GroupMember
{
    public class CreatePostModel : PageModel
    {
        [BindProperty]
        public PostDto PostDto { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var apiUrl = "https://localhost:44340/api/GroupMember/CreatePost";
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync(apiUrl, PostDto);

            if (response.IsSuccessStatusCode)
            {
                var createdPost = await response.Content.ReadFromJsonAsync<Post>();
                // Handle the created post if needed
                return RedirectToPage("GetPost", new { id = createdPost.PostId });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error creating post.");
                return Page();
            }
        }
    }
}
