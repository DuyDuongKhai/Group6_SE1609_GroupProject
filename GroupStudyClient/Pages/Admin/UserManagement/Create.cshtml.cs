using System;
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
using Microsoft.EntityFrameworkCore;

namespace GroupStudyClient.Pages.Admin.UserManagement
{
    public class CreateModel : PageModel
    {

        private readonly IHttpClientFactory _clientFactory;

        public CreateModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;

        }

        public IActionResult OnGet()
        {
            List<string> roles = new List<string>();
            roles.Add("GroupAdmin");
            roles.Add("User");
            ViewData["Role"] = new SelectList(roles);
            return Page();
        }

        [BindProperty]
        public User User { get; set; }

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
                TempData["IsCreateSuccess"] = true; // Set the success flag in TempData
                TempData["SuccessMesssages"] = "Create user success";
                // The flowerBouquet data was successfully updated, handle the success as needed
                return RedirectToPage("/Admin/UserManagement/Index");
            }
            else
            {
                // Handle error if needed
                List<string> roles = new List<string>();
                roles.Add("GroupAdmin");
                roles.Add("User");
                ViewData["Role"] = new SelectList(roles);
                ModelState.AddModelError(string.Empty, "An error occurred while updating the user.");
                return Page();
            }
        }
    }
}
