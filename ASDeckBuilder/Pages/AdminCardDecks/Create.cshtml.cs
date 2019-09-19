using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ASDeckBuilder.Data;

namespace ASDeckBuilder.Pages.AdminCardDecks
{
    public class CreateModel : PageModel
    {
        private readonly ASDeckBuilder.Data.ApplicationDbContext _context;

        public CreateModel(ASDeckBuilder.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CardId"] = new SelectList(_context.Cards, "CardId", "Name");
        ViewData["DeckId"] = new SelectList(_context.Decks, "DeckId", "DeckId");
            return Page();
        }

        [BindProperty]
        public CardDecks CardDecks { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CardDecks.Add(CardDecks);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}