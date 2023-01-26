using Warehouse.ProductService.Application.Models;

namespace Warehouse.ProductService.Application.Contracts.Services
{
    public interface IOrderDetailsService
    {
        void ValidateOrderDetails(Product product, OrderDetails orderDetails, bool isReserveProduct = true);
    }
}
