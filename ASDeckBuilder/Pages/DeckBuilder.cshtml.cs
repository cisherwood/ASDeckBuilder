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

        // Create properties for data
        public ICollection<Card> Cards { get; set; }
        public ICollection<Tags> Tags { get; set; }
        public ICollection<Categories> Categories { get; set; }
        public ICollection<CardCategories> CardCategories { get; set; }
        public ICollection<CardTags> CardTags { get; set; }
        public ICollection<Decks> Decks { get; set; }



        public IActionResult OnGet()
        {
            Cards = _context.Cards.ToList();
            Tags = _context.Tags.ToList();
            Categories = _context.Categories.ToList();
            CardCategories = _context.CardCategories.ToList();
            CardTags = _context.CardTags.ToList();
            Decks = _context.Decks.ToList();

            return Page();

        }

        public string GetCardCost(int cardId)
        {
            string cardCost = "";
            Card card = Cards.Where(x => x.CardId == cardId).FirstOrDefault();
            List<CardTags> cardTags = CardTags.Where(x => x.CardId == card.CardId).ToList();

            foreach(CardTags t in cardTags)
            {
                switch (t.Tag.Name)
                {
                    case "1":
                        cardCost = "1";
                        break;
                    case "2":
                        cardCost = "2";
                        break;
                    case "3":
                        cardCost = "3";
                        break;
                    case "4":
                        cardCost = "4";
                        break;
                    case "5":
                        cardCost = "5";
                        break;
                    case "6":
                        cardCost = "6";
                        break;
                    case "7":
                        cardCost = "7";
                        break;
                    case "8":
                        cardCost = "8";
                        break;
                    case "9":
                        cardCost = "9";
                        break;
                    case "10":
                        cardCost = "10";
                        break;
                    case "x":
                        cardCost = "x";
                        break;
                    case "X":
                        cardCost = "x";
                        break;
                    default:
                        break;




                }


            }

            return cardCost;
        }

        public string GetCardColor(int cardId)
        {
            string cardColor = "";
            Card card = Cards.Where(x => x.CardId == cardId).FirstOrDefault();
            List<CardCategories> cardCategories = CardCategories.Where(x => x.CardId == card.CardId).ToList();

            foreach (CardCategories t in cardCategories)
            {
                switch (t.Categories.Name)
                {
                    case "Argent":
                        cardColor = "Argent";
                        break;
                    case "Fire":
                        cardColor = "Fire";
                        break;
                    case "Water":
                        cardColor = "Water";
                        break;
                    case "Air":
                        cardColor = "Air";
                        break;
                    case "Light":
                        cardColor = "Light";
                        break;
                    case "Dark":
                        cardColor = "Dark";
                        break;
                    default:
                        break;
                }
            }
            return cardColor;
        }
    }
}