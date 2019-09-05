using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASDeckBuilder.Data;

namespace ASDeckBuilder.Pages
{
    public class CardSearchModel : PageModel
    {
        private readonly ASDeckBuilder.Data.ApplicationDbContext _context;

        public CardSearchModel(ASDeckBuilder.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Card> Card { get;set; }

        public async Task OnGetAsync()
        {
            Card = await _context.Cards.ToListAsync();
        }
    }
}
