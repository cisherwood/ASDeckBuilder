﻿using System;
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
            card = _context.Cards.FirstOrDefault();

            return Page();
        }


        public IActionResult OnPost(string id)
        {
            int cardId = Convert.ToInt32(id);

            card = _context.Cards.Where(x => x.CardId == cardId).FirstOrDefault();

            return Page();
        }

        
    }
}