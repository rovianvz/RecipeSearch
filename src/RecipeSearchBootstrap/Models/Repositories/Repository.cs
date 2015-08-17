using RecipeSearchBootstrap.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace RecipeSearchBootstrap.Models.Repositories
{
    public class Repository<T> where T : class
    {
        private RecipeSearchDbContext context = null;

        public RecipeSearchDbContext GetContext()
        {
            return context;
        }
        protected DbSet<T> DbSet
        {
            get;
            set;
        }

        public Repository()
        {
            context = new RecipeSearchDbContext();
            DbSet = context.Set<T>();
        }

        public Repository(RecipeSearchDbContext context)
        {
            this.context = context;
            DbSet = context.Set<T>();
        }

        public List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public T Add(T entity)
        {
            return DbSet.Add(entity);
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public DbEntityEntry Entry(T entity)
        {
            return context.Entry(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}