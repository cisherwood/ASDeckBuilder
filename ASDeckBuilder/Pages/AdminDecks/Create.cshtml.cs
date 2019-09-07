using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ASDeckBuilder.Data;

namespace ASDeckBuilder.Pages.AdminDecks
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
            return Page();
        }

        [BindProperty]
        public Decks Decks { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Decks.Add(Decks);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}