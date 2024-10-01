using ShoesApp.Entidades.Entities;
using System.Linq.Expressions;

namespace ShoesApp.Servicios.Interfaces
{
    public interface IBrandsService
    {
        IEnumerable<Brand>? GetAll(Expression<Func<Brand, bool>>? filter = null,
            Func<IQueryable<Brand>, IOrderedQueryable<Brand>>? orderBy = null,
            string? propertiesNames = null);
        Brand? GetById(Expression<Func<Brand, bool>> filter,
            bool tracked = true, string? propertiesNames = null);
        void Save(Brand brand);
        void Remove(Brand brand);
        bool Exist(Brand brand);
        bool ItsRelated(int id);

    }
}
