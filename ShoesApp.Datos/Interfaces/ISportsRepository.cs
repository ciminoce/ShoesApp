using ShoesApp.Entidades.Entities;

namespace ShoesApp.Datos.Interfaces
{
    public interface ISportsRepository : IRepository<Sport>
    {
        void Update(Sport sport);
        bool Exist(Sport sport);
        bool ItsRelated(int id);
    }
}
