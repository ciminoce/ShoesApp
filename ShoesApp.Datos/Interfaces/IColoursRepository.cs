using ShoesApp.Entidades.Entities;

namespace ShoesApp.Datos.Interfaces
{
    public interface IColoursRepository : IRepository<Colour>
    {
        void Update(Colour colour);
        bool Exist(Colour colour);
        bool ItsRelated(int id);
    }
}
