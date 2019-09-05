using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASDeckBuilder.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASDeckBuilder.Pages
{
    public class DeckBuilderModel : PageModel
    {
        private readonly ASDeckBuilder.Data.ApplicationDbContext _context;

        public DeckBuilderModel(ASDeckBuilder.Data.ApplicationDbContext context)
        {
            _context = context;


        }

        public Card Cards { get; set; }
        public Tags Tags { get; set; }

        public Categories Categories { get; set; }

        public CardCategories CardCategories { get; set; }

        public CardTags CardTags { get; set; }





        public void OnGet()
        {

        }
    }
}