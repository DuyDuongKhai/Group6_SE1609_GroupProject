using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;

namespace GroupStudyClient.Pages.Admin.GroupManagement
{
    public class CreateModel : PageModel
    {
        private readonly BusinessObject.Models.GroupStudyContext _context;

        public CreateModel(BusinessObject.Models.GroupStudyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["GroupAdminId"] = new SelectList(_context.Users, "UserId", "UserId");
            return Page();
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

            _context.Groups.Add(Group);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
