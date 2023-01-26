using AutoMapper;
using Warehouse.ProductService.Application.Contracts.Services;
using WarehouseService.Application.Contracts.Repositories;
using WarehouseService.Domain.Entities.Base;

namespace Warehouse.ProductService.Application.Services
{
    public class GenericService<TModel, TEntity> : IGenericService<TModel>
        where TModel : class
        where TEntity : EntityBase
    {
        protected readonly IGenericRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TModel> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _repository.Delete(id, cancellationToken);

            return _mapper.Map<TModel>(result);
        }

        public async Task<IEnumerable<TModel>> Get(CancellationToken cancellationToken)
        {
            var result = await _repository.Get(cancellationToken);

            return _mapper.Map<IEnumerable<TModel>>(result);
        }

        public async Task<TModel> GetById(int id, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetByPredicate(x => x.Id == id, cancellationToken);
            var entity = entities.FirstOrDefault();

            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<TModel> Insert(TModel model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(model);

            var result = await _repository.Insert(entity, cancellationToken);

            return _mapper.Map<TModel>(result);
        }

        public async Task<TModel> Update(TModel model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(model);

            var result = await _repository.Update(entity, cancellationToken);

            return _mapper.Map<TModel>(result);
        }
    }
}
