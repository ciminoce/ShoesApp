using ShoesApp.Entidades.Entities;

namespace ShoesApp.Datos.Interfaces
{
    public interface IBrandsRepository : IRepository<Brand>
    {
        void Update(Brand brand);
        bool Exist(Brand brand);
        bool ItsRelated(int id);
    }
}
