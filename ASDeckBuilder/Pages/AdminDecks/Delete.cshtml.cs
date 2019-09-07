using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASDeckBuilder.Data;

namespace ASDeckBuilder.Pages.AdminDecks
{
    public class DeleteModel : PageModel
    {
        private readonly ASDeckBuilder.Data.ApplicationDbContext _context;

        public DeleteModel(ASDeckBuilder.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Decks Decks { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Decks = await _context.Decks.FirstOrDefaultAsync(m => m.DeckId == id);

            if (Decks == null)
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

            Decks = await _context.Decks.FindAsync(id);

            if (Decks != null)
            {
                _context.Decks.Remove(Decks);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
