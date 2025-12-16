using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Buda_Paul_Lab2.Data;
using Buda_Paul_Lab2.Models;

namespace Buda_Paul_Lab2.Pages.Borrowings
{
    public class DetailsModel : PageModel
    {
        private readonly Buda_Paul_Lab2.Data.Buda_Paul_Lab2Context _context;

        public DetailsModel(Buda_Paul_Lab2.Data.Buda_Paul_Lab2Context context)
        {
            _context = context;
        }

        public Borrowing Borrowing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowing
                .Include(b => b.Member)
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (borrowing is not null)
            {
                Borrowing = borrowing;

                return Page();
            }

            return NotFound();
        }
    }
}
