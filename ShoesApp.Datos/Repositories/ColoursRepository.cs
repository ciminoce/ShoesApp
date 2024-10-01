using Gardens2024.Data.Repositories;
using ShoesApp.Datos.Interfaces;
using ShoesApp.Entidades.Entities;

namespace ShoesApp.Datos.Repositories
{
    public class ColoursRepository : Repository<Colour>, IColoursRepository
    {
        private readonly ShoesDbContext _context;

        public ColoursRepository(ShoesDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool Exist(Colour colour)
        {
            if (colour == null)
            {
                throw new ArgumentNullException(nameof(colour));
            }

            if (colour.ColourId == 0)
            {
                return _context.Colours.Any(c => c.ColourName == colour.ColourName);
            }
            return _context.Colours.Any(c => c.ColourName == colour.ColourName && c.ColourId != colour.ColourId);
        }


        public bool ItsRelated(int id)
        {
            //return _context.Shoes.Any(p => p.ColourId == id);
            return true;
        }

        public void Update(Colour colour)
        {
            if (colour == null)
            {
                throw new ArgumentNullException(nameof(colour));
            }

            _context.Colours.Update(colour);

        }
    }
}
