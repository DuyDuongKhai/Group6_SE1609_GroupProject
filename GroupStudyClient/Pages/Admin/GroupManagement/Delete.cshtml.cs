using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;

namespace GroupStudyClient.Pages.Admin.GroupManagement
{
    public class DeleteModel : PageModel
    {
        private readonly BusinessObject.Models.GroupStudyContext _context;

        public DeleteModel(BusinessObject.Models.GroupStudyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Group Group { get; set; }

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
        //    return Page();
        //}

        //public async Task<IActionResult> OnPostAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    Group = await _context.Groups.FindAsync(id);

        //    if (Group != null)
        //    {
        //        _context.Groups.Remove(Group);
        //        await _context.SaveChangesAsync();
        //    }

        //    return RedirectToPage("./Index");
        //}
    }
}
