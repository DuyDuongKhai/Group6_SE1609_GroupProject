using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using System.Text.Json;
using System.Net.Http;
using System.Text;

namespace GroupStudyClient.Pages.Admin.UserManagement
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public DeleteModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;

        }

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var httpClient = _clientFactory.CreateClient();

            // Send login request to the API
            var response = await httpClient.GetAsync($"https://localhost:44340/api/Users/{id}");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                User = JsonSerializer.Deserialize<User>(apiResponse, options);
            }


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var httpClient = _clientFactory.CreateClient();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var jsonContent = JsonSerializer.Serialize(User);

            HttpResponseMessage response = await httpClient.DeleteAsync($"https://localhost:44340/api/Users/{User.UserId}\r\n");
            if (response.IsSuccessStatusCode)
            {
                // The flowerBouquet data was successfully updated, handle the success as needed
                return RedirectToPage("/Admin/UserManagement/Index");
            }
            else
            {
                // Handle error if needed
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the user.");
                return Page();
            }
        }
    }
}
