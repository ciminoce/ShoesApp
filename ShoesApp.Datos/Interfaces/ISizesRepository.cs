using ShoesApp.Entidades.Entities;

namespace ShoesApp.Datos.Interfaces
{
    public interface ISizesRepository : IRepository<Size>
    {
        void Update(Size size);
        bool Exist(Size size);
        bool ItsRelated(int id);
    }
}
