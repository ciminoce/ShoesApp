using ShoesApp.Datos;
using ShoesApp.Datos.Interfaces;
using ShoesApp.Entidades.Entities;
using ShoesApp.Servicios.Interfaces;
using System.Linq.Expressions;

namespace ShoesApp.Servicios.Services
{
    public class BrandsService : IBrandsService
    {
        private readonly IBrandsRepository? _repository;
        private readonly IUnitOfWork? _unitOfWork;

        public BrandsService(IBrandsRepository? repository,
            IUnitOfWork? unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Remove(Brand brand)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                _repository?.Remove(brand);
                _unitOfWork?.Commit();

            }
            catch (Exception)
            {
                _unitOfWork?.Rollback();
                throw;
            }
        }

        public bool Exist(Brand brand)
        {
            if (_repository is null)
            {
                throw new ApplicationException("Dependencies not loaded!!");
            }

            return _repository.Exist(brand);
        }

        public IEnumerable<Brand>? GetAll(Expression<Func<Brand, bool>>? filter = null,
            Func<IQueryable<Brand>, IOrderedQueryable<Brand>>? orderBy = null,
            string? propertiesNames = null)
        {
            return _repository?.GetAll(filter, orderBy);
        }

        public Brand? GetById(Expression<Func<Brand, bool>> filter, bool tracked = true, string? propertiesNames = null)
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

        public void Save(Brand brand)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (brand.BrandId == 0)
                {
                    _repository?.Add(brand);
                }
                else
                {
                    _repository?.Update(brand);
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
