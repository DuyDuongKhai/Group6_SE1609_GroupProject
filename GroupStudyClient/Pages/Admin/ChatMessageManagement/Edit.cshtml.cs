using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;

namespace GroupStudyClient.Pages.Admin.ChatMessageManagement
{
    public class EditModel : PageModel
    {
        private readonly BusinessObject.Models.GroupStudyContext _context;

        public EditModel(BusinessObject.Models.GroupStudyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ChatMessage ChatMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ChatMessage = await _context.ChatMessages
                .Include(c => c.Group)
                .Include(c => c.User).FirstOrDefaultAsync(m => m.MessageId == id);

            if (ChatMessage == null)
            {
                return NotFound();
            }
           ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupId");
           ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ChatMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatMessageExists(ChatMessage.MessageId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ChatMessageExists(int id)
        {
            return _context.ChatMessages.Any(e => e.MessageId == id);
        }
    }
}
