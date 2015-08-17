using RecipeSearchBootstrap.Models;
using RecipeSearchBootstrap.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RecipeSearchBootstrap.Controllers
{
    [Authorize(Roles="Admin")]
    public class ManageRecipesController : Controller
    {

        private RecipesRepository repository = new RecipesRepository();
        
        // GET: ManageRecipes
        public ActionResult Index()
        {
            return View(repository.GetAll());
        }

        // GET: ManageRecipes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipes recipes = repository.Get((int)id);
            if (recipes == null)
            {
                return HttpNotFound();
            }
            return View(recipes);
        }

        // POST: ManageRecipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Recipes recipes = repository.Get((int)id);
            repository.Remove(recipes);
            repository.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: ManageRecipes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipes recipes = repository.Get((int)id);
            if (recipes == null)
            {
                return HttpNotFound();
            }
            return View(recipes);
        }

        // POST: ManageRecipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecipesID,Name,Instructions, Ingredients, Portion, Time")] Recipes recipe)
        {
            if (ModelState.IsValid)
            {
                var ingredientsRepository = new IngredientsRepository(repository.GetContext());
                recipe.IngredientsList = ingredientsRepository.GenerateIngredientList(recipe);
                if(!recipe.Approved)
                {
                    recipe.Approved = true;
                }
                repository.Entry(recipe).State = EntityState.Modified;
                repository.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recipe);
        }

        
    }
}