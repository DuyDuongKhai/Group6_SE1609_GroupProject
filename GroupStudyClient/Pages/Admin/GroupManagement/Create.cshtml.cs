﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace GroupStudyClient.Pages.Admin.GroupManagement
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public CreateModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;

        }

        public async Task<IActionResult> OnGet()
        {
            var httpClient = _clientFactory.CreateClient();

            // Send login request to the API
            var response = await httpClient.GetAsync("https://localhost:44340/api/Users/GroupAdmin");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<User> users = JsonSerializer.Deserialize<List<User>>(apiResponse, options);
                ViewData["GroupAdminId"] = new SelectList(users, "UserId", "UserId");
                return Page();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "ERROR");
                return Page();
            }
            
        }

        [BindProperty]
        public Group Group { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var httpClient = _clientFactory.CreateClient();

            // Send login request to the API
            var jsonContent = JsonSerializer.Serialize(User);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync($"https://localhost:44340/api/Users", content);
            if (response.IsSuccessStatusCode)
            {
                // The flowerBouquet data was successfully updated, handle the success as needed
                return RedirectToPage("/Admin/GroupManagement/Index");
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
