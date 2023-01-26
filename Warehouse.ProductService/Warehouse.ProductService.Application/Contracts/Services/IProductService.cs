using Warehouse.ProductService.Application.Models;

namespace Warehouse.ProductService.Application.Contracts.Services
{
    public interface IProductService : IGenericService<Product>
    {
        Task<Product> ReserveProduct(OrderDetails orderDetails, CancellationToken cancellationToken);
        Task<Product> CancelProductReservation(OrderDetails orderDetails, CancellationToken cancellationToken);
    }
}
