using Warehouse.ProductService.Application.Contracts.Services;
using Warehouse.ProductService.Application.Models;

namespace Warehouse.ProductService.Application.Services
{
    public class OrderDetailsService : IOrderDetailsService
    {
        public OrderDetailsService() { }

        public void ValidateOrderDetails(Product product, OrderDetails orderDetails, bool isReserveProduct = true)
        {
            if (orderDetails is null) throw new ArgumentNullException(nameof(orderDetails));

            if (isReserveProduct && orderDetails.ProductAmount > product.Quantity) throw new ArgumentOutOfRangeException(nameof(orderDetails));
        }
    }
}
