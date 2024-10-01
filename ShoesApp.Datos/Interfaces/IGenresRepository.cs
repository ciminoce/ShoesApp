using ShoesApp.Entidades.Entities;

namespace ShoesApp.Datos.Interfaces
{
    public interface IGenresRepository : IRepository<Genre>
    {
        void Update(Genre genre);
        bool Exist(Genre genre);
        bool ItsRelated(int id);
    }
}
