using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using System.Net.Http;
using System.Net.Http.Json;
using GroupStudyAPI.Models;
using System.Text.Json;

namespace GroupStudyClient.Pages.Admin.UserManagement
{
    public class IndexModel : PageModel
    {

        private readonly IHttpClientFactory _clientFactory;

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IList<User> User { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = _clientFactory.CreateClient();

            // Send login request to the API
            var response = await httpClient.GetAsync("https://localhost:44340/api/Users");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                User = JsonSerializer.Deserialize<List<User>>(apiResponse, options);
                //if (apiResponse.Success)
                //{
                //    User = JsonConvert.DeserializeObject<List<User>>(apiResponse);

                //    if (!string.IsNullOrEmpty(User))
                //    {


                //    }
                //    else
                //    {
                //        ModelState.AddModelError(string.Empty, "Không nhận được token từ phản hồi.");
                //    }
                //}
                //else
                //{
                //    ModelState.AddModelError(string.Empty, apiResponse.Message);
                //}

            }
            return Page();

        }
    }
}
