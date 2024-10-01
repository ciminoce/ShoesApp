using ShoesApp.Entidades.Dtos.Shoes;
using ShoesApp.Entidades.Entities;
using System.Linq.Expressions;

namespace ShoesApp.Servicios.Interfaces
{
    public interface IShoesService
    {
        IEnumerable<Shoe>? GetAll(Expression<Func<Shoe, bool>>? filter = null,
    Func<IQueryable<Shoe>, IOrderedQueryable<Shoe>>? orderBy = null,
    string? propertiesNames = null);
        Shoe? GetById(Expression<Func<Shoe, bool>> filter,
            bool tracked = true, string? propertiesNames = null);
        void Save(Shoe shoe);
        void Remove(Shoe shoe);
        bool Exist(Shoe shoe);
        bool ItsRelated(int id);
    }
}
