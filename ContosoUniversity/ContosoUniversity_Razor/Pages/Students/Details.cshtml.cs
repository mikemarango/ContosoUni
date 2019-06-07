﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity_Razor.Data;
using ContosoUniversity_Razor.Models;

namespace ContosoUniversity_Razor.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly SchoolContext _context;

        public DetailsModel(SchoolContext context)
        {
            _context = context;
        }

        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Student
                        .Include(s => s.Enrollments)
                            .ThenInclude(e => e.Course)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.ID == id);

            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
