using Microsoft.EntityFrameworkCore.Storage;
using Warehouse.ProductService.Application.Contracts.Transaction;
using WarehouseService.Infrastructure;

namespace Warehouse.ProductService.Infrastructure.Transaction
{
    public class DatabaseContextTransaction : IDatabaseContextTransaction
    {
        private readonly DatabaseContext _context;

        public DatabaseContextTransaction(DatabaseContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            _context = context;
        }

        public async Task<IDbContextTransaction?> ExecuteAsync(CancellationToken cancellationToken)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            return transaction;
        }
    }
}
