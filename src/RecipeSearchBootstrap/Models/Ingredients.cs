using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RecipeSearchBootstrap.Models
{
    public class Ingredients
    {
        public int IngredientsID { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual List<Recipes> Recipes { get; set; }
    }
}