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
