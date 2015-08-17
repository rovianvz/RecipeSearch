using RecipeSearchBootstrap.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipeSearchBootstrap.Controllers
{
    public class HomeController : Controller
    {
        RecipesRepository recipes = new RecipesRepository();

        public ActionResult Index()
        {
            ViewBag.Title="RecipeSearch";
            return View(recipes.GetAll().Take(8));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}