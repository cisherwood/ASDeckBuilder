using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASDeckBuilder.Data
{
    public class CardCategories
    {

        [Key]
        public int CardCategoryId { get; set; }


        [Required]
        public int CardId { get; set; }

        [Required]
        public int CategoryId { get; set; }


        public Card Card { get; set; }
        public Categories Categories { get; set; }

    }
}
