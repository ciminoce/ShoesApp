using Gardens2024.Data.Repositories;
using ShoesApp.Datos.Interfaces;
using ShoesApp.Entidades.Entities;

namespace ShoesApp.Datos.Repositories
{
    public class GenresRepository : Repository<Genre>, IGenresRepository
    {
        private readonly ShoesDbContext _context;

        public GenresRepository(ShoesDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool Exist(Genre genre)
        {
            if (genre == null)
            {
                throw new ArgumentNullException(nameof(genre));
            }

            if (genre.GenreId == 0)
            {
                return _context.Genres.Any(c => c.GenreName == genre.GenreName);
            }
            return _context.Genres.Any(c => c.GenreName == genre.GenreName && c.GenreId != genre.GenreId);
        }


        public bool ItsRelated(int id)
        {
            return _context.Shoes.Any(p => p.GenreId == id);
        }

        public void Update(Genre genre)
        {
            if (genre == null)
            {
                throw new ArgumentNullException(nameof(genre));
            }

            _context.Genres.Update(genre);

        }
    }
}
