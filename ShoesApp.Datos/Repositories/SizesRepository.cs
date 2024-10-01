using Gardens2024.Data.Repositories;
using ShoesApp.Datos.Interfaces;
using ShoesApp.Entidades.Entities;

namespace ShoesApp.Datos.Repositories
{
    public class SizesRepository : Repository<Size>, ISizesRepository
    {
        private readonly ShoesDbContext _context;

        public SizesRepository(ShoesDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool Exist(Size size)
        {
            if (size == null)
            {
                throw new ArgumentNullException(nameof(size));
            }

            if (size.SizeId == 0)
            {
                return _context.Sizes.Any(c => c.SizeNumber == size.SizeNumber);
            }
            return _context.Sizes.Any(c => c.SizeNumber == size.SizeNumber && c.SizeId != size.SizeId);
        }


        public bool ItsRelated(int id)
        {
            return true;
        }

        public void Update(Size size)
        {
            if (size == null)
            {
                throw new ArgumentNullException(nameof(size));
            }

            _context.Sizes.Update(size);

        }
    }
}
