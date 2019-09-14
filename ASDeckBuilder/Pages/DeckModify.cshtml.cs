using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASDeckBuilder.Data;

namespace ASDeckBuilder.Pages
{
    public class DeckModifyModel : PageModel
    {
        private readonly ASDeckBuilder.Data.ApplicationDbContext _context;

        public DeckModifyModel(ASDeckBuilder.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CardDecks CardDecks { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string modifyAmount)
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
           ViewData["CardId"] = new SelectList(_context.Cards, "CardId", "Name");
           ViewData["DeckId"] = new SelectList(_context.Decks, "DeckId", "DeckId");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CardDecks).State = EntityState.Modified;
            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardDecksExists(CardDecks.CardDeckId))
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

        private bool CardDecksExists(int id)
        {
            return _context.CardDecks.Any(e => e.CardDeckId == id);
        }
    }
}
