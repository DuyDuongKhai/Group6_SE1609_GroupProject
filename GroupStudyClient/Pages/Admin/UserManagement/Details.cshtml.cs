using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using System.Net.Http;
using System.Text.Json;

namespace GroupStudyClient.Pages.Admin.UserManagement
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;


        public DetailsModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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
    }
}
