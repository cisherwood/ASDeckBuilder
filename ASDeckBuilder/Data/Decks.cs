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
        public int CardId { get; set; }
        public short Quantity { get; set; }

        public Card Card { get; set; }
    }
}
