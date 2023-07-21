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

namespace GroupStudyClient.Pages.Admin.GroupManagement
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public DeleteModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;

        }

        [BindProperty]
        public Group Group { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var httpClient = _clientFactory.CreateClient();

            // Send login request to the API
            var response = await httpClient.GetAsync($"https://localhost:44340/api/Groups/{id}");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                Group = JsonSerializer.Deserialize<Group>(apiResponse, options);
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

            var jsonContent = JsonSerializer.Serialize(Group);

            HttpResponseMessage response = await httpClient.DeleteAsync($"https://localhost:44340/api/Groups/{Group.GroupId}\r\n");
            if (response.IsSuccessStatusCode)
            {
                TempData["IsCreateSuccess"] = true; // Set the success flag in TempData
                TempData["SuccessMesssages"] = "Delete group success";
                // The flowerBouquet data was successfully updated, handle the success as needed
                return RedirectToPage("/Admin/GroupManagement/Index");
            }
            else
            {
                // Handle error if needed
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the group.");
                return Page();
            }
        }
    }
}
