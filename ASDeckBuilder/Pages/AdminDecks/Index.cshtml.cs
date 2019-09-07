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
    public class IndexModel : PageModel
    {
        private readonly ASDeckBuilder.Data.ApplicationDbContext _context;

        public IndexModel(ASDeckBuilder.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Decks> Decks { get;set; }

        public async Task OnGetAsync()
        {
            Decks = await _context.Decks.ToListAsync();
        }
    }
}
