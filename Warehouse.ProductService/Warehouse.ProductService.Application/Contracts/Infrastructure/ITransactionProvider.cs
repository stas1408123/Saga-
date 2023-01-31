using Microsoft.EntityFrameworkCore.Storage;

namespace Warehouse.ProductService.Application.Contracts.Infrastructure
{
    public interface ITransactionProvider
    {
        Task<IDbContextTransaction> BeginTransactionAsync(string transactionId, CancellationToken cancellationToken);
        IDbContextTransaction? GetContextTransaction(string transactionId);
    }
}
