using ShoesApp.Entidades.Entities;

namespace ShoesApp.Datos.Interfaces
{
    public interface IShoesRepository:IRepository<Shoe>
    {
        void Update(Shoe shoe);
        bool Exist(Shoe shoe);
        bool ItsRelated(int id);

    }
}
