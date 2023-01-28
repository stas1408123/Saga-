using Warehouse.ProductService.Application.Contracts.Services;
using Warehouse.ProductService.Application.Infrastructure.Exceptions;
using Warehouse.ProductService.Application.Models;

namespace Warehouse.ProductService.Application.Services
{
    public class OrderDetailsService : IOrderDetailsService
    {
        public OrderDetailsService() { }

        public void ValidateOrderDetails(Product product, OrderDetails orderDetails, bool isReserveProduct = true)
        {
            if (product is null || orderDetails is null || (isReserveProduct && orderDetails.ProductAmount > product.Quantity))
            {
                throw new InvalidOrderDetailsException();
            }
        }
    }
}
