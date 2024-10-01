using Gardens2024.Data.Repositories;
using ShoesApp.Datos.Interfaces;
using ShoesApp.Entidades.Entities;

namespace ShoesApp.Datos.Repositories
{
    public class BrandsRepository : Repository<Brand>, IBrandsRepository
    {
        private readonly ShoesDbContext _context;

        public BrandsRepository(ShoesDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool Exist(Brand brand)
        {
            if (brand == null)
            {
                throw new ArgumentNullException(nameof(brand));
            }

            if (brand.BrandId == 0)
            {
                return _context.Brands.Any(c => c.BrandName == brand.BrandName);
            }
            return _context.Brands.Any(c => c.BrandName == brand.BrandName && c.BrandId != brand.BrandId);
        }


        public bool ItsRelated(int id)
        {
            return _context.Shoes.Any(p => p.BrandId == id);
        }

        public void Update(Brand brand)
        {
            if (brand == null)
            {
                throw new ArgumentNullException(nameof(brand));
            }

            _context.Brands.Update(brand);

        }
    }
}
