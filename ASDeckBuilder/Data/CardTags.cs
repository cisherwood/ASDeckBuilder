using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASDeckBuilder.Data
{
    public class CardTags
    {

        [Key]
        public int CardTagId { get; set; }

        [Required]
        public int CardId { get; set; }
        

        [Required]
        public int TagId { get; set; }



        public Tags Tag { get; set; }
        public Card Card { get; set; }
    }
}
