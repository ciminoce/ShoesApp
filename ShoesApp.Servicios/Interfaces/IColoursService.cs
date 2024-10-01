using ShoesApp.Entidades.Entities;
using System.Linq.Expressions;

namespace ShoesApp.Servicios.Interfaces
{
    public interface IColoursService
    {
        IEnumerable<Colour>? GetAll(Expression<Func<Colour, bool>>? filter = null,
            Func<IQueryable<Colour>, IOrderedQueryable<Colour>>? orderBy = null,
            string? propertiesNames = null);
        Colour? GetById(Expression<Func<Colour, bool>> filter,
            bool tracked = true, string? propertiesNames = null);
        void Save(Colour colour);
        void Remove(Colour colour);
        bool Exist(Colour colour);
        bool ItsRelated(int id);

    }
}
