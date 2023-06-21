using System.Net.Http;
using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace GroupStudyClient.Pages.Users
{
    public class DashboardProfileModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public User User { get; set; }

        public DashboardProfileModel(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Index");
            }

            var apiUrlWithUserId = $"https://localhost:44340/api/User/{userId}";

            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync(apiUrlWithUserId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                User = JsonConvert.DeserializeObject<User>(content);
            }
            else
            {
                // Handle error if needed
                User = new User();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(User updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Index");
            }

            var apiUrlWithUserId = $"https://localhost:44340/api/User/{userId}";

            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.PutAsJsonAsync(apiUrlWithUserId, updatedUser);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }
            else
            {
                // Handle error if needed
                ModelState.AddModelError(string.Empty, "An error occurred while updating the user.");
                return Page();
            }
        }
    }
}
