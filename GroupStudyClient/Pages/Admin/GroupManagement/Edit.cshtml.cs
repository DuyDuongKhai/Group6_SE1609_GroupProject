using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;

namespace GroupStudyClient.Pages.Admin.GroupManagement
{
    public class EditModel : PageModel
    {
        private readonly BusinessObject.Models.GroupStudyContext _context;

        public EditModel(BusinessObject.Models.GroupStudyContext context)
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
