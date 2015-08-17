using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RecipeSearchBootstrap.Context;
using RecipeSearchBootstrap.Models;
using RecipeSearchBootstrap.Models.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RecipeSearchBootstrap.Controllers
{

    public class RecipesController : Controller
    {
        private RecipesRepository repository = new RecipesRepository();
        private IngredientsRepository ingredientsRepository = new IngredientsRepository();

        private UserManager<User> manager;

        public RecipesController()
        {

            manager = new UserManager<User>(new UserStore<User>(repository.GetContext()));

        }

        // GET: Recipes
        public ActionResult Index()
        {
            var recipes = new List<Recipes>() ;

            return View(recipes);
        }
        
        public PartialViewResult _GetSearchResults(string searchString, string Command)
        {
            short limit = 0;
            switch(Command)
            {
                case "perfect":
                    limit = 0;
                    break;
                case "oneMissing":
                    limit = 1;
                    break;
                case "twoMissing":
                    limit = 2;
                    break;
                case "threeMissing":
                    limit = 3;
                    break;
            }
            ViewBag.SearchParameters = searchString.Split(',').ToList().Select(m => m.Trim()).ToList();
            var recipes = repository.GetListByName(searchString, limit);

            return PartialView("_GetSearchResults", recipes);
        }
        // GET: Recipes
        public ActionResult List()
        {
            return View(repository.GetAll());
        }

        // GET: Recipes/Details/5
        public ActionResult Details(int? id)
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

        // GET: Recipes/Create

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "RecipesID,Name,Instructions, Ingredients, Portion, Time")] Recipes recipes)
        {

            if (ModelState.IsValid)
            {
                recipes.Approved = false;
                recipes.User = manager.FindById(User.Identity.GetUserId());
                repository.Add(recipes);
                repository.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recipes);
        }

        // GET: Recipes/Edit/5
        [Authorize]
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
            if(recipes.User != manager.FindById(User.Identity.GetUserId()))
            {
                return RedirectToAction("Index");
            }
            return View(recipes);
        }

        

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "RecipesID,Name,Instructions")] Recipes recipes)
        {
            if (recipes.User != manager.FindById(User.Identity.GetUserId()))
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                recipes.Approved = false;
                repository.Entry(recipes).State = EntityState.Modified;
                repository.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recipes);
        }

        /// GET: Recipes/Delete/5
        [Authorize]
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
            if (recipes.User != manager.FindById(User.Identity.GetUserId()))
            {
                return RedirectToAction("Index");
            }
            return View(recipes);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Recipes recipes = repository.Get((int)id);

            if (recipes.User != manager.FindById(User.Identity.GetUserId()))
            {
                return RedirectToAction("Index");
            }

            repository.Remove(recipes);
            repository.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
