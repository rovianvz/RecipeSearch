using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipeSearchBootstrap.Models
{
    public class Recipes
    {
        public int RecipesID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Instructions { get; set; }

        public byte[] Images { get; set; }

        [Required]
        public string Ingredients { get; set; }

        [Required]
        public short Time { get; set; }

        [Required]
        public short Portions { get; set; }

        public virtual List<Ingredients> IngredientsList { get; set; }

        public virtual User User { get; set; }

        public bool Approved { get; set; }


    }
}