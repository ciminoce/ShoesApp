using ShoesApp.Datos;
using ShoesApp.Datos.Interfaces;
using ShoesApp.Entidades.Dtos.Shoes;
using ShoesApp.Entidades.Entities;
using ShoesApp.Servicios.Interfaces;
using System.Linq.Expressions;

namespace ShoesApp.Servicios.Services
{
    public class ShoeColoursService : IShoeColoursService
    {
        private readonly IShoeColoursRepository? _repository;
        private readonly IUnitOfWork? _unitOfWork;

        public ShoeColoursService(IShoeColoursRepository? repository,
            IUnitOfWork? unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Remove(ShoeColour shoecolour)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                _repository?.Remove(shoecolour);
                _unitOfWork?.Commit();

            }
            catch (Exception)
            {
                _unitOfWork?.Rollback();
                throw;
            }
        }

        public bool Exist(ShoeColour shoecolour)
        {
            if (_repository is null)
            {
                throw new ApplicationException("Dependencies not loaded!!");
            }

            return _repository.Exist(shoecolour);
        }

        public IEnumerable<ShoeColour>? GetAll(Expression<Func<ShoeColour, bool>>? filter = null,
            Func<IQueryable<ShoeColour>, IOrderedQueryable<ShoeColour>>? orderBy = null,
            string? propertiesNames = null)
        {
            return _repository?.GetAll(filter, orderBy, propertiesNames);
        }

        public ShoeColour? GetById(Expression<Func<ShoeColour, bool>> filter, bool tracked = true, string? propertiesNames = null)
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

        public void Save(ShoeColour shoeColour)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (shoeColour.ShoeColourId == 0)
                {
                    _repository?.Add(shoeColour);
                }
                else
                {
                    _repository?.Update(shoeColour);
                }
                _unitOfWork?.Commit();

            }
            catch (Exception)
            {
                _unitOfWork?.Rollback();
                throw;
            }
        }

        public void AssignColorsAndPricesToShoe(ShoeColourDto shoeColor)
        {
            try
            {
                _unitOfWork?.BeginTransaction();

                ShoeColour shoeColourToSave = new ShoeColour()
                {
                    ShoeId = shoeColor.ShoeId,
                    ColourId = shoeColor.ColourId,
                    PriceAdjustment = shoeColor.Price,
                };
                _repository?.Add(shoeColourToSave);
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
