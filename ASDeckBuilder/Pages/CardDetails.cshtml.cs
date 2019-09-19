using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASDeckBuilder.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASDeckBuilder.Pages
{
    public class CardDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public Card card { get; set; }

        public CardDetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {


            return Page();
        }


        public IActionResult OnPost([FromBody]string id)
        {
            int cardId = Convert.ToInt32(id);

            card = _context.Cards.Where(x => x.CardId == cardId).FirstOrDefault();
            card.CardEffects = _context.CardEffects.Where(x => x.CardId == card.CardId).ToList();

            return Page();
        }

        
    }
}