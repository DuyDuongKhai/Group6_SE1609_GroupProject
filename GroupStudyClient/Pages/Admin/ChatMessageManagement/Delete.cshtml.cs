using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;

namespace GroupStudyClient.Pages.Admin.ChatMessageManagement
{
    public class DeleteModel : PageModel
    {
        private readonly BusinessObject.Models.GroupStudyContext _context;

        public DeleteModel(BusinessObject.Models.GroupStudyContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ChatMessage = await _context.ChatMessages.FindAsync(id);

            if (ChatMessage != null)
            {
                _context.ChatMessages.Remove(ChatMessage);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
