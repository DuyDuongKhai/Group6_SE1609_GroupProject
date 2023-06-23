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
    public class DetailsModel : PageModel
    {
        private readonly BusinessObject.Models.GroupStudyContext _context;

        public DetailsModel(BusinessObject.Models.GroupStudyContext context)
        {
            _context = context;
        }

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
    }
}
