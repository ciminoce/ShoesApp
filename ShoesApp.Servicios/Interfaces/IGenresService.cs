using ShoesApp.Entidades.Entities;
using System.Linq.Expressions;

namespace ShoesApp.Servicios.Interfaces
{
    public interface IGenresService
    {
        IEnumerable<Genre>? GetAll(Expression<Func<Genre, bool>>? filter = null,
            Func<IQueryable<Genre>, IOrderedQueryable<Genre>>? orderBy = null,
            string? propertiesNames = null);
        Genre? GetById(Expression<Func<Genre, bool>> filter,
            bool tracked = true, string? propertiesNames = null);
        void Save(Genre genre);
        void Remove(Genre genre);
        bool Exist(Genre genre);
        bool ItsRelated(int id);

    }
}
