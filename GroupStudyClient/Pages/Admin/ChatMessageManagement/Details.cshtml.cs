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
    public class DetailsModel : PageModel
    {
        private readonly BusinessObject.Models.GroupStudyContext _context;

        public DetailsModel(BusinessObject.Models.GroupStudyContext context)
        {
            _context = context;
        }

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
    }
}
