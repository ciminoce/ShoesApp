using ShoesApp.Datos;
using ShoesApp.Datos.Interfaces;
using ShoesApp.Entidades.Dtos.Shoes;
using ShoesApp.Entidades.Entities;
using ShoesApp.Servicios.Interfaces;
using System.Linq.Expressions;

namespace ShoesApp.Servicios.Services
{
    public class ShoesService : IShoesService
    {
        private readonly IShoesRepository? _repository;
        private readonly IUnitOfWork? _unitOfWork;

        public ShoesService(IShoesRepository? repository,
            IUnitOfWork? unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Remove(Shoe shoe)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                _repository?.Remove(shoe);
                _unitOfWork?.Commit();

            }
            catch (Exception)
            {
                _unitOfWork?.Rollback();
                throw;
            }
        }

        public bool Exist(Shoe shoe)
        {
            if (_repository is null)
            {
                throw new ApplicationException("Dependencies not loaded!!");
            }

            return _repository.Exist(shoe);
        }

        public IEnumerable<Shoe>? GetAll(Expression<Func<Shoe, bool>>? filter = null,
            Func<IQueryable<Shoe>, IOrderedQueryable<Shoe>>? orderBy = null,
            string? propertiesNames = null)
        {
            return _repository?.GetAll(filter, orderBy, propertiesNames);
        }

        public Shoe? GetById(Expression<Func<Shoe, bool>> filter, bool tracked = true, string? propertiesNames = null)
        {
            return _repository?.GetById(filter, tracked, propertiesNames);
        }

        public bool ItsRelated(int id)
        {
            if (_repository is null)
            {
                throw new ApplicationException("Dependencies not loaded!!");
            }

            return _repository.ItsRelated(id);
        }

        public void Save(Shoe shoe)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (shoe.ShoeId == 0)
                {
                    _repository?.Add(shoe);
                }
                else
                {
                    _repository?.Update(shoe);
                }
                _unitOfWork?.Commit();

            }
            catch (Exception)
            {
                _unitOfWork?.Rollback();
                throw;
            }
        }

        public void AssignColorsToShoe(int shoeId, List<int> selectedColourIds)
        {
            throw new NotImplementedException();
        }

        public void AssignColorsAndPricesToShoe(int shoeId, List<ShoeColourDto> shoeColors)
        {
            throw new NotImplementedException();
        }

        public void AddColorToShoe(int shoeId, int value1, decimal value2)
        {
            throw new NotImplementedException();
        }
    }
}
