using ASDeckBuilder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ASDeckBuilder.Models
{
    public class DeckBuilderViewModel
    {
        public int DeckId { get; set; }

        public List<CardDecks> CardDecks { get; set; }

        public string CardJSON { get; set; }
    }
}
