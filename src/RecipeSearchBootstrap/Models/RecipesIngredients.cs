using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeSearchBootstrap.Models
{
    public class RecipesIngredients
    {
        public int RecipesID { get; set; }
        public int IngredientsID { get; set; }
        public virtual Ingredients Ingredients { get; set; }
        public virtual Recipes Recipes { get; set; }
        public string Quantity { get; set; }
    }
}