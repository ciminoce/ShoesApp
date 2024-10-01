using ShoesApp.Datos;
using ShoesApp.Datos.Interfaces;
using ShoesApp.Entidades.Entities;
using ShoesApp.Servicios.Interfaces;
using System.Linq.Expressions;

namespace ShoesApp.Servicios.Services
{
    public class GenresService : IGenresService
    {
        private readonly IGenresRepository? _repository;
        private readonly IUnitOfWork? _unitOfWork;

        public GenresService(IGenresRepository? repository,
            IUnitOfWork? unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Remove(Genre genre)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                _repository?.Remove(genre);
                _unitOfWork?.Commit();

            }
            catch (Exception)
            {
                _unitOfWork?.Rollback();
                throw;
            }
        }

        public bool Exist(Genre genre)
        {
            if (_repository is null)
            {
                throw new ApplicationException("Dependencies not loaded!!");
            }

            return _repository.Exist(genre);
        }

        public IEnumerable<Genre>? GetAll(Expression<Func<Genre, bool>>? filter = null,
            Func<IQueryable<Genre>, IOrderedQueryable<Genre>>? orderBy = null,
            string? propertiesNames = null)
        {
            return _repository?.GetAll(filter, orderBy);
        }

        public Genre? GetById(Expression<Func<Genre, bool>> filter, bool tracked = true, string? propertiesNames = null)
        {
            return _repository?.GetById(filter, tracked);
        }

        public bool ItsRelated(int id)
        {
            if (_repository is null)
            {
                throw new ApplicationException("Dependencies not loaded!!");
            }

            return _repository.ItsRelated(id);
        }

        public void Save(Genre genre)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (genre.GenreId == 0)
                {
                    _repository?.Add(genre);
                }
                else
                {
                    _repository?.Update(genre);
                }
                _unitOfWork?.Commit();

            }
            catch (Exception)
            {
                _unitOfWork?.Rollback();
                throw;
            }
        }
    }
}
