using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RecipeSearchBootstrap.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace RecipeSearchBootstrap.Context
{
    public class RecipeSearchDbContextInitializer : DropCreateDatabaseIfModelChanges<RecipeSearchDbContext>
    {

        string text = "Bacon ipsum dolor amet shoulder prosciutto tail tenderloin sausage t-bone cow pork loin rump. Brisket porchetta jerky, shankle fatback beef ribs t-bone filet mignon biltong strip steak. Porchetta tongue cow turducken salami rump frankfurter ham tri-tip prosciutto picanha turkey. Jerky short ribs rump, fatback ball tip doner sirloin beef ribs pig swine kevin pork loin pork chop.\n"
                    + "Shank corned beef ham pancetta. Alcatra pork belly prosciutto, swine ham boudin frankfurter turkey rump. Tail kielbasa frankfurter, flank doner shank sausage hamburger swine beef ribs meatloaf. Pastrami tongue pork belly jerky. Meatball doner corned beef sirloin pig, brisket tenderloin capicola jerky kielbasa pork belly pork chuck. Pork chop cupim shoulder, capicola chicken prosciutto pork belly sausage venison beef ribs meatball corned beef salami landjaeger.";

        Image img = Image.FromFile(@"C:\Users\Rovian\documents\visual studio 2013\Projects\RecipeSearchBootstrap\RecipeSearchBootstrap\images\food.jpg");
        byte[] arr;

        protected override void Seed(RecipeSearchDbContext context)
        {

            var user = GenerateUsers(context);

            GenerateRecipes(context, user);

            base.Seed(context);
        }

        private static User GenerateUsers(RecipeSearchDbContext context)
        {
            var UserManager = new UserManager<User>(new UserStore<User>(context));

            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            string name = "rovianvz@gmail.com";
            string password = "rvz542714";

            //Create Role Admin if it does not exist

            if (!RoleManager.RoleExists("Admin"))
            {
                var roleresult = RoleManager.Create(new IdentityRole("Admin"));
            }

            //Create User=Admin with password=123456

            var user = new User();
            user.UserName = name;
            user.FullName = "Rovian Voelz Veronez";

            var adminresult = UserManager.Create(user, password);

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, "Admin");
            }

            return user;
        }

        private void GenerateRecipes(RecipeSearchDbContext context, User user)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                arr = ms.ToArray();
            }

            var presunto = context.Ingredients.Add(new Ingredients() { Name = "presunto" });
            var queijo = context.Ingredients.Add(new Ingredients() { Name = "queijo" });
            var sal = context.Ingredients.Add(new Ingredients() { Name = "sal" });
            var ovo = context.Ingredients.Add(new Ingredients() { Name = "ovo" });
            var oregano = context.Ingredients.Add(new Ingredients() { Name = "orégano" });
            var manteiga = context.Ingredients.Add(new Ingredients() { Name = "manteiga" });
            var carne_moida = context.Ingredients.Add(new Ingredients() { Name = "carne moída" });
            var pimenta = context.Ingredients.Add(new Ingredients() { Name = "pimenta" });
            var pao = context.Ingredients.Add(new Ingredients() { Name = "pão" });
            var espaguete = context.Ingredients.Add(new Ingredients() { Name = "espaguete" });
            var bacon = context.Ingredients.Add(new Ingredients() { Name = "bacon" });
            var penne = context.Ingredients.Add(new Ingredients() { Name = "penne" });
            var queijo_ralado = context.Ingredients.Add(new Ingredients() { Name = "queijo ralado" });
            var creme_de_leite = context.Ingredients.Add(new Ingredients() { Name = "creme de leite" });


            List<Recipes> recipes = new List<Recipes>
            {
                new Recipes() { User = user, Images = arr, Time = 10, Approved = true, Portions = 2, Ingredients = "teste", Name = "Torrada", Instructions = text, IngredientsList = new List<Ingredients> {pao, presunto, queijo}},
                new Recipes() { User = user, Images = arr, Time = 10, Approved = true, Portions = 2, Ingredients = "teste", Name = "Ovos mexidos", Instructions = text, IngredientsList = new List<Ingredients> { ovo, presunto, queijo}},
                new Recipes() { User = user, Images = arr, Time = 10, Approved = true, Portions = 2, Ingredients = "teste", Name = "Sanduíche simples", Instructions = text, IngredientsList = new List<Ingredients> {pao, presunto, queijo, manteiga}},
                new Recipes() { User = user, Images = arr, Time = 10, Approved = true, Portions = 2, Ingredients = "teste", Name = "Massa carbonara", Instructions = text, IngredientsList = new List<Ingredients>{bacon, ovo, manteiga, queijo_ralado, penne, creme_de_leite}},
                new Recipes() { User = user, Images = arr, Time = 10, Approved = true, Portions = 2, Ingredients = "teste", Name = "Enroladinho de presunto e queijo", Instructions = text, IngredientsList = new List<Ingredients>{presunto, queijo}},
                new Recipes() { User = user, Images = arr, Time = 10, Approved = true, Portions = 2, Ingredients = "teste", Name = "Enroladinho de presunto", Instructions = text, IngredientsList = new List<Ingredients>{presunto}},
                new Recipes() { User = user, Images = arr, Time = 10, Approved = true, Portions = 2, Ingredients = "teste", Name = "Pão com ovo", Instructions = text, IngredientsList = new List<Ingredients>{pao, ovo}}

            };
            var toddynho = context.Recipes.Add(new Recipes() { User = user, Images = arr, Time = 10, Approved = true, Portions = 2, Ingredients = "teste", Name = "Toddynhio", Instructions = text });
            context.Ingredients.Add(new Ingredients() { Name = "leite", Recipes = new List<Recipes> { toddynho } });
            context.Ingredients.Add(new Ingredients() { Name = "Nescau", Recipes = new List<Recipes> { toddynho } });
            context.Recipes.AddRange(recipes);
            context.SaveChanges();
        }
    }
}