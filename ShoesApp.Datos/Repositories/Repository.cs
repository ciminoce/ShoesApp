using Microsoft.EntityFrameworkCore;
using ShoesApp.Datos;
using ShoesApp.Datos.Interfaces;
using System.Linq.Expressions;

namespace Gardens2024.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ShoesDbContext _context;
        internal DbSet<T> dbSet;
        public Repository(ShoesDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, 
            string? propertiesNames = null)
        {
            IQueryable<T> query = dbSet.AsNoTracking();
            if (propertiesNames != null)
            {
                var properties = propertiesNames.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var property in properties)
                {
                    query = query.Include(property);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }
        public T? GetById(Expression<Func<T, bool>> filter,bool tracked=false, string? propertiesNames=null)
        {
            IQueryable<T> query = dbSet;
            query=query.Where(filter);
            if (propertiesNames != null)
            {
                var properties = propertiesNames.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var property in properties)
                {
                    query = query.Include(property);
                }
            }

            return tracked ?query.FirstOrDefault():query.AsNoTracking().FirstOrDefault();
        }


        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
