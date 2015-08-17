using Microsoft.AspNet.Identity.EntityFramework;
using RecipeSearchBootstrap.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RecipeSearchBootstrap.Context
{
    public class RecipeSearchDbContext : IdentityDbContext<User>
    {
        public RecipeSearchDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }

        public static RecipeSearchDbContext Create()
        {
            return new RecipeSearchDbContext();
        }
    }

}