using ShoesApp.Entidades.Entities;

namespace ShoesApp.Datos.Interfaces
{
    public interface IShoeColoursRepository : IRepository<ShoeColour>
    {
        void Update(ShoeColour shoeColour);
        bool Exist(ShoeColour shoeColour);
        bool ItsRelated(int id);
        void AssignColorsAndPricesToShoe(ShoeColour shoeColour);

    }
}
