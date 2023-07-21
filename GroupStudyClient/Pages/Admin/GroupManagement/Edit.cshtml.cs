using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace GroupStudyClient.Pages.Admin.GroupManagement
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public EditModel(IHttpClientFactory clientFactory)
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

        public async Task<IActionResult> OnPostAsync()
        {
            var httpClient = _clientFactory.CreateClient();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var jsonContent = JsonSerializer.Serialize(Group);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");



            HttpResponseMessage response = await httpClient.PutAsync($"https://localhost:44340/api/Groups/{Group.GroupId}\r\n", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["IsCreateSuccess"] = true; // Set the success flag in TempData
                TempData["SuccessMesssages"] = "Edit group success";
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

        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    Group = await _context.Groups
        //        .Include(@ => @.GroupAdmin).FirstOrDefaultAsync(m => m.GroupId == id);

        //    if (Group == null)
        //    {
        //        return NotFound();
        //    }
        //   ViewData["GroupAdminId"] = new SelectList(_context.Users, "UserId", "UserId");
        //    return Page();
        //}

        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see https://aka.ms/RazorPagesCRUD.
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    _context.Attach(Group).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GroupExists(Group.GroupId))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return RedirectToPage("./Index");
        //}

        //private bool GroupExists(int id)
        //{
        //    return _context.Groups.Any(e => e.GroupId == id);
        //}
    }
}
