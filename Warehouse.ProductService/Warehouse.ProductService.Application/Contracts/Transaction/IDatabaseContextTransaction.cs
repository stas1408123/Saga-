using Microsoft.EntityFrameworkCore.Storage;

namespace Warehouse.ProductService.Application.Contracts.Transaction
{
    public interface IDatabaseContextTransaction
    {
        Task<IDbContextTransaction?> ExecuteAsync(CancellationToken cancellationToken);
    }
}
