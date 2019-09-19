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
    public class DetailsModel : PageModel
    {
        private readonly ASDeckBuilder.Data.ApplicationDbContext _context;

        public DetailsModel(ASDeckBuilder.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
