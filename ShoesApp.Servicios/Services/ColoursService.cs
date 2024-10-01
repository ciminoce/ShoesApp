using ShoesApp.Datos;
using ShoesApp.Datos.Interfaces;
using ShoesApp.Entidades.Entities;
using ShoesApp.Servicios.Interfaces;
using System.Linq.Expressions;

namespace ShoesApp.Servicios.Services
{
    public class ColoursService : IColoursService
    {
        private readonly IColoursRepository? _repository;
        private readonly IUnitOfWork? _unitOfWork;

        public ColoursService(IColoursRepository? repository,
            IUnitOfWork? unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Remove(Colour colour)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                _repository?.Remove(colour);
                _unitOfWork?.Commit();

            }
            catch (Exception)
            {
                _unitOfWork?.Rollback();
                throw;
            }
        }

        public bool Exist(Colour colour)
        {
            if (_repository is null)
            {
                throw new ApplicationException("Dependencies not loaded!!");
            }

            return _repository.Exist(colour);
        }

        public IEnumerable<Colour>? GetAll(Expression<Func<Colour, bool>>? filter = null,
            Func<IQueryable<Colour>, IOrderedQueryable<Colour>>? orderBy = null,
            string? propertiesNames = null)
        {
            return _repository?.GetAll(filter, orderBy);
        }

        public Colour? GetById(Expression<Func<Colour, bool>> filter, bool tracked = true, string? propertiesNames = null)
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

        public void Save(Colour colour)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (colour.ColourId == 0)
                {
                    _repository?.Add(colour);
                }
                else
                {
                    _repository?.Update(colour);
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
