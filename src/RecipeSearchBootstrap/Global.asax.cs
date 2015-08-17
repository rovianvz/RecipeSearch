using Microsoft.AspNet.Identity.EntityFramework;
using RecipeSearchBootstrap.App_Start;
using RecipeSearchBootstrap.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RecipeSearchBootstrap
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<RecipeSearchDbContext>(new RecipeSearchDbContextInitializer());
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}
