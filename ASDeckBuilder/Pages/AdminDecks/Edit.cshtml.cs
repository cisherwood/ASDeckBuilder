using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASDeckBuilder.Data;

namespace ASDeckBuilder.Pages.AdminDecks
{
    public class EditModel : PageModel
    {
        private readonly ASDeckBuilder.Data.ApplicationDbContext _context;

        public EditModel(ASDeckBuilder.Data.ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Decks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DecksExists(Decks.DeckId))
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

        private bool DecksExists(int id)
        {
            return _context.Decks.Any(e => e.DeckId == id);
        }
    }
}
