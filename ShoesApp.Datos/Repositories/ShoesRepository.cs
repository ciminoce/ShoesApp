using Gardens2024.Data.Repositories;
using ShoesApp.Datos.Interfaces;
using ShoesApp.Entidades.Entities;

namespace ShoesApp.Datos.Repositories
{
    public class ShoesRepository : Repository<Shoe>, IShoesRepository
    {
        private readonly ShoesDbContext _context;

        public ShoesRepository(ShoesDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool Exist(Shoe shoe)
        {
            if (shoe == null)
            {
                throw new ArgumentNullException(nameof(shoe));
            }

            if (shoe.ShoeId == 0)
            {
                return _context.Shoes.Any(c => c.Model == shoe.Model);
            }
            return _context.Shoes.Any(c => c.Model == shoe.Model && c.ShoeId != shoe.ShoeId);
        }


        public bool ItsRelated(int id)
        {
            return _context.Shoes.Any(p => p.ShoeId == id);
        }

        public void Update(Shoe shoe)
        {
            if (shoe == null)
            {
                throw new ArgumentNullException(nameof(shoe));
            }

            _context.Shoes.Update(shoe);

        }
    }
}
