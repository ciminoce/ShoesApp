using Gardens2024.Data.Repositories;
using ShoesApp.Datos.Interfaces;
using ShoesApp.Entidades.Entities;

namespace ShoesApp.Datos.Repositories
{
    public class SportsRepository : Repository<Sport>, ISportsRepository
    {
        private readonly ShoesDbContext _context;

        public SportsRepository(ShoesDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool Exist(Sport sport)
        {
            if (sport == null)
            {
                throw new ArgumentNullException(nameof(sport));
            }

            if (sport.SportId == 0)
            {
                return _context.Sports.Any(c => c.SportName == sport.SportName);
            }
            return _context.Sports.Any(c => c.SportName == sport.SportName && c.SportId != sport.SportId);
        }


        public bool ItsRelated(int id)
        {
            return _context.Shoes.Any(p => p.SportId == id);
        }

        public void Update(Sport sport)
        {
            if (sport == null)
            {
                throw new ArgumentNullException(nameof(sport));
            }

            _context.Sports.Update(sport);

        }
    }
}
