namespace Warehouse.ProductService.Application.Contracts.Services
{
    public interface IGenericService<TModel> where TModel : class
    {
        Task<TModel> Insert(TModel model, CancellationToken cancellationToken);
        Task<IEnumerable<TModel>> Get(CancellationToken cancellationToken);
        Task<TModel> Update(TModel model, CancellationToken cancellationToken);
        Task<TModel> Delete(int id, CancellationToken cancellationToken);
        Task<TModel> GetById(int id, CancellationToken cancellationToken);
    }
}
