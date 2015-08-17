using LinqKit;
using RecipeSearchBootstrap.Context;
using RecipeSearchBootstrap.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace RecipeSearchBootstrap.Models.Repositories
{
    public class IngredientsRepository : Repository<Ingredients>
    {

        public IngredientsRepository() : base() { }
        public IngredientsRepository(RecipeSearchDbContext context) : base(context) { }

        public List<Ingredients> GetByRecipe(Recipes recipe)
        {
            var list = DbSet.Where(a => a.Recipes.Any(b => b.RecipesID == recipe.RecipesID)).ToList() ;
            return list;
        }

        public Ingredients GetByName(string name)
        {
            var ingredient = DbSet.Where(a => a.Name.Equals(name));
            if (ingredient.Count() <= 0)
                return null;
            else
                return ingredient.First();
        }

        public List<Ingredients> GenerateIngredientList(Recipes recipe)
        {

            //Remove all current ingredients from recipe
            var recipeIngredients = GetByRecipe(recipe);
            foreach(var ing in recipeIngredients)
            {
                recipe.IngredientsList.Remove(ing);
            }

            
            
            var ingredients = new List<Ingredients>();
            var reader = new StringReader(recipe.Ingredients);
            string line;

            while (null != (line = reader.ReadLine()))
            {

                var name = line.Split('-')[1].Trim();
                var ing = GetByName(name);

                if (ing == null)
                {
                    var newIng = new Ingredients() { Name = name };
                    ing = Add(newIng);
                }
                ingredients.Add(ing);
            }

            SaveChanges();
            return ingredients;
        }
    }
}