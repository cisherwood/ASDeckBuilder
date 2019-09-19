using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASDeckBuilder.Data
{
    public class Decks
    {
        [Key]
        public int DeckId { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }


        public ICollection<CardDecks> CardDecks { get; set; }

    }
}
