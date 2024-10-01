using ShoesApp.Entidades.Entities;
using System.Linq.Expressions;

namespace ShoesApp.Servicios.Interfaces
{
    public interface ISizesService
    {
        IEnumerable<Size>? GetAll(Expression<Func<Size, bool>>? filter = null,
            Func<IQueryable<Size>, IOrderedQueryable<Size>>? orderBy = null,
            string? propertiesNames = null);
        Size? GetById(Expression<Func<Size, bool>> filter,
            bool tracked = true, string? propertiesNames = null);
        void Save(Size size);
        void Remove(Size size);
        bool Exist(Size size);
        bool ItsRelated(int id);

    }
}
