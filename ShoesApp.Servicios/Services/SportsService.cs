using ShoesApp.Datos;
using ShoesApp.Datos.Interfaces;
using ShoesApp.Entidades.Entities;
using ShoesApp.Servicios.Interfaces;
using System.Linq.Expressions;

namespace ShoesApp.Servicios.Services
{
    public class SportsService : ISportsService
    {
        private readonly ISportsRepository? _repository;
        private readonly IUnitOfWork? _unitOfWork;

        public SportsService(ISportsRepository? repository,
            IUnitOfWork? unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Remove(Sport sport)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                _repository?.Remove(sport);
                _unitOfWork?.Commit();

            }
            catch (Exception)
            {
                _unitOfWork?.Rollback();
                throw;
            }
        }

        public bool Exist(Sport sport)
        {
            if (_repository is null)
            {
                throw new ApplicationException("Dependencies not loaded!!");
            }

            return _repository.Exist(sport);
        }

        public IEnumerable<Sport>? GetAll(Expression<Func<Sport, bool>>? filter = null,
            Func<IQueryable<Sport>, IOrderedQueryable<Sport>>? orderBy = null,
            string? propertiesNames = null)
        {
            return _repository?.GetAll(filter, orderBy);
        }

        public Sport? GetById(Expression<Func<Sport, bool>> filter, bool tracked = true, string? propertiesNames = null)
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

        public void Save(Sport sport)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (sport.SportId == 0)
                {
                    _repository?.Add(sport);
                }
                else
                {
                    _repository?.Update(sport);
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
