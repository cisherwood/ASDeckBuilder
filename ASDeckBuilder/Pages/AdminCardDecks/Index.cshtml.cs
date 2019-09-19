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
    public class IndexModel : PageModel
    {
        private readonly ASDeckBuilder.Data.ApplicationDbContext _context;

        public IndexModel(ASDeckBuilder.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CardDecks> CardDecks { get;set; }

        public async Task OnGetAsync()
        {
            CardDecks = await _context.CardDecks
                .Include(c => c.Card)
                .Include(c => c.Decks).ToListAsync();
        }
    }
}
