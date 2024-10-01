using ShoesApp.Entidades.Entities;
using System.Linq.Expressions;

namespace ShoesApp.Servicios.Interfaces
{
    public interface ISportsService
    {
        IEnumerable<Sport>? GetAll(Expression<Func<Sport, bool>>? filter = null,
            Func<IQueryable<Sport>, IOrderedQueryable<Sport>>? orderBy = null,
            string? propertiesNames = null);
        Sport? GetById(Expression<Func<Sport, bool>> filter,
            bool tracked = true, string? propertiesNames = null);
        void Save(Sport sport);
        void Remove(Sport sport);
        bool Exist(Sport sport);
        bool ItsRelated(int id);

    }
}
