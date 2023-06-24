using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;

namespace GroupStudyClient.Pages.Admin.ChatMessageManagement
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
        ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupId");
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return Page();
        }

        [BindProperty]
        public ChatMessage ChatMessage { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ChatMessages.Add(ChatMessage);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
