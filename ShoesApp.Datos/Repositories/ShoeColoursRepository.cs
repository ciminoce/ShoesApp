using Gardens2024.Data.Repositories;
using ShoesApp.Datos.Interfaces;
using ShoesApp.Entidades.Dtos.Shoes;
using ShoesApp.Entidades.Entities;

namespace ShoesApp.Datos.Repositories
{
    public class ShoeColoursRepository : Repository<ShoeColour>, IShoeColoursRepository
    {
        private readonly ShoesDbContext _context;

        public ShoeColoursRepository(ShoesDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AssignColorsAndPricesToShoe(ShoeColour shoeColour)
        {
            _context.ShoeColours.Add(shoeColour);
        }

        public bool Exist(ShoeColour shoecolour)
        {
            if (shoecolour == null)
            {
                throw new ArgumentNullException(nameof(shoecolour));
            }

            if (shoecolour.ShoeColourId == 0)
            {
                return _context.ShoeColours.Any(c => c.ShoeId == shoecolour.ShoeId && 
                            c.ColourId==shoecolour.ColourId);
            }
            return _context.ShoeColours.Any(c => c.ShoeId == shoecolour.ShoeId &&
                            c.ColourId == shoecolour.ColourId &&
                            c.ShoeColourId != shoecolour.ShoeColourId);
        }


        public bool ItsRelated(int id)
        {
            return _context.ShoeColours.Any(p => p.ShoeColourId == id);
        }

        public void Update(ShoeColour shoecolour)
        {
            if (shoecolour == null)
            {
                throw new ArgumentNullException(nameof(shoecolour));
            }

            _context.ShoeColours.Update(shoecolour);

        }
    }
}
