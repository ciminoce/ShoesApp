using ShoesApp.Datos;
using ShoesApp.Datos.Interfaces;
using ShoesApp.Entidades.Entities;
using ShoesApp.Servicios.Interfaces;
using System.Linq.Expressions;

namespace ShoesApp.Servicios.Services
{
    public class SizesService : ISizesService
    {
        private readonly ISizesRepository? _repository;
        private readonly IUnitOfWork? _unitOfWork;

        public SizesService(ISizesRepository? repository,
            IUnitOfWork? unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Remove(Size size)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                _repository?.Remove(size);
                _unitOfWork?.Commit();

            }
            catch (Exception)
            {
                _unitOfWork?.Rollback();
                throw;
            }
        }

        public bool Exist(Size size)
        {
            if (_repository is null)
            {
                throw new ApplicationException("Dependencies not loaded!!");
            }

            return _repository.Exist(size);
        }

        public IEnumerable<Size>? GetAll(Expression<Func<Size, bool>>? filter = null,
            Func<IQueryable<Size>, IOrderedQueryable<Size>>? orderBy = null,
            string? propertiesNames = null)
        {
            return _repository?.GetAll(filter, orderBy);
        }

        public Size? GetById(Expression<Func<Size, bool>> filter, bool tracked = true, string? propertiesNames = null)
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

        public void Save(Size size)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (size.SizeId == 0)
                {
                    _repository?.Add(size);
                }
                else
                {
                    _repository?.Update(size);
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
