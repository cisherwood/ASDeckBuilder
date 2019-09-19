using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASDeckBuilder.Data;

namespace ASDeckBuilder.Pages.AdminCardDecks
{
    public class DeleteModel : PageModel
    {
        private readonly ASDeckBuilder.Data.ApplicationDbContext _context;

        public DeleteModel(ASDeckBuilder.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CardDecks CardDecks { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CardDecks = await _context.CardDecks
                .Include(c => c.Card)
                .Include(c => c.Decks).FirstOrDefaultAsync(m => m.CardDeckId == id);

            if (CardDecks == null)
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

            CardDecks = await _context.CardDecks.FindAsync(id);

            if (CardDecks != null)
            {
                _context.CardDecks.Remove(CardDecks);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
