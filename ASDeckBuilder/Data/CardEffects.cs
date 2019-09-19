using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASDeckBuilder.Data
{
    public class CardEffects
    {
        [Key]
        public int CardEffectsId { get; set; }

        [Required]
        public int CardId { get; set; }


        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }


        public Card Card { get; set; }
    }
}
