using Warehouse.Contracts.DTOs;
using Warehouse.Contracts.DTOs.Enums;
using Warehouse.ProductService.Application.Models;

namespace Warehouse.ProductService.Application.Contracts.Strategy
{
    public interface IProcessingStockStatusStartegy
    {
        bool IsApplictable(StockStatus status);
        Task ProcessProduct(ProductDTO product, OrderDetails orderDetails, CancellationToken cancellationToken);
    }
}
