using ShoesApp.Entidades.Dtos.Shoes;
using ShoesApp.Entidades.Entities;
using System.Linq.Expressions;

namespace ShoesApp.Servicios.Interfaces
{
    public interface IShoeColoursService
    {
        IEnumerable<ShoeColour>? GetAll(Expression<Func<ShoeColour, bool>>? filter = null,
            Func<IQueryable<ShoeColour>, IOrderedQueryable<ShoeColour>>? orderBy = null,
            string? propertiesNames = null);
        ShoeColour? GetById(Expression<Func<ShoeColour, bool>> filter,
            bool tracked = true, string? propertiesNames = null);
        void Save(ShoeColour shoecolour);
        void Remove(ShoeColour shoecolour);
        bool Exist(ShoeColour shoecolour);
        bool ItsRelated(int id);
        void AssignColorsAndPricesToShoe(ShoeColourDto shoeColor);

    }
}
