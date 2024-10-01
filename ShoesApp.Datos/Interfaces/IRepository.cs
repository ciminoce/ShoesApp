using System.Linq.Expressions;

namespace ShoesApp.Datos.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? propertiesNames = null);
        T? GetById(Expression<Func<T, bool>> filter, bool tracked = false, string? propertiesNames = null);
        void Add(T entity);
        void Remove(T entity);

    }
}
