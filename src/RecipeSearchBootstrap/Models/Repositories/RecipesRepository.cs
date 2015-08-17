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
    public class RecipesRepository : Repository<Recipes>
    {
        public List<Recipes> GetByName(string name)
        {
            return DbSet.Where(a => a.Name.Contains(name)).ToList();
        }

        public List<Recipes> GetListByName(string parameters, short limit)
        {

            if (parameters == null || parameters.Length == 0)
                return new List<Recipes>();
            else
            {
                List<string> ingredients = parameters.Split(',').ToList().Select(m => m.Trim()).ToList();

                var par = ProduceWithRecursion(ingredients);

                var perfectResult = new List<Recipes>();
                var missingResult = new List<Recipes>();
                foreach(var ingredient in par)
                {
                    List<Recipes> perf = GetPerfectQuery(ingredient).ToList();
                    perfectResult.AddRange(perf);

                    List<Recipes> miss = GetOtherPossibilities(ingredient, limit).ToList();
                    missingResult.AddRange(miss);
                }
                var result = perfectResult.Union(missingResult);
                return result.OrderBy(x => x.IngredientsList.Count).ToList();
            }
        }

        private IQueryable<Recipes> GetOtherPossibilities(List<string> parameters, short limit)
        {
            IQueryable<Recipes> query = DbSet;

            var predicate = PredicateBuilder.False<Recipes>();

            foreach (string keyword in parameters)
            {
                predicate = predicate.Or(p => p.IngredientsList.Any(b => b.Name == keyword));
            }
            predicate = predicate.And(p => p.IngredientsList.Count < parameters.Count + limit);
            predicate = predicate.And(p => p.Approved == true);

            return query.AsExpandable().Where(predicate);

        }

        private IQueryable<Recipes> GetPerfectQuery(List<string> parameters)
        {
            IQueryable<Recipes> query = DbSet;
                        
            foreach (string filter in parameters)
            {
                query = query.Where(a => a.IngredientsList.Any(b => b.Name == filter));
            }
            query = query.Where(a => a.IngredientsList.Count == parameters.Count);
            query = query.Where(a => a.Approved == true);
            
            return query;
        }

        private IEnumerable<List<string>> ProduceWithRecursion(List<string> allValues)
        {
            for (var i = 0; i < (1 << allValues.Count); i++)
            {
                yield return ConstructSetFromBits(i).Select(n => allValues[n]).ToList();
            }
        }

        private IEnumerable<int> ConstructSetFromBits(int i)
        {
            var n = 0;
            for (; i != 0; i /= 2)
            {
                if ((i & 1) != 0) yield return n;
                n++;
            }
        }

        
    }
}