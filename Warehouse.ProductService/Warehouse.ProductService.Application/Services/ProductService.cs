using AutoMapper;
using Warehouse.ProductService.Application.Contracts.Repositories;
using Warehouse.ProductService.Application.Contracts.Services;
using Warehouse.ProductService.Application.Models;
using WarehouseService.Domain.Entities;
using WarehouseService.Domain.Enums;

namespace Warehouse.ProductService.Application.Services
{
    public class ProductService : GenericService<Product, ProductEntity>, IProductService
    {
        private readonly ICategoryService _categoryService;
        private readonly IOrderDetailsService _orderDetailsService;

        public ProductService(IProductRepository repository, ICategoryService categoryService, IOrderDetailsService orderDetailsService, IMapper mapper) : base(repository, mapper)
        {
            ArgumentNullException.ThrowIfNull(categoryService);
            ArgumentNullException.ThrowIfNull(orderDetailsService);

            _categoryService = categoryService;
            _orderDetailsService = orderDetailsService;
        }

        public override async Task<Product> Insert(Product model, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<ProductEntity>(model);
            var category = await _categoryService.GetById(model.CategoryId, cancellationToken);

            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            product.StockStatus = CalculateStockStatus(product.Quantity, category!);

            var result = await _repository.Insert(product, cancellationToken);
            return _mapper.Map<Product>(result);
        }

        public async Task<Product> ReserveProduct(OrderDetails orderDetails, CancellationToken cancellationToken)
        {
            var product = await GetById(orderDetails.ProductId, cancellationToken);

            _orderDetailsService.ValidateOrderDetails(product, orderDetails);

            product.Quantity -= orderDetails.ProductAmount;

            product.StockStatus = CalculateStockStatus(product.Quantity, product.Category);
            var updatedProduct = await _repository.Update(_mapper.Map<ProductEntity>(product), cancellationToken);

            return _mapper.Map<Product>(updatedProduct);
        }

        public async Task<Product> CancelProductReservation(OrderDetails orderDetails, CancellationToken cancellationToken)
        {
            var product = await GetById(orderDetails.ProductId, cancellationToken);

            _orderDetailsService.ValidateOrderDetails(product, orderDetails, isReserveProduct: false);

            product.Quantity += orderDetails.ProductAmount;

            product.StockStatus = CalculateStockStatus(product.Quantity, product.Category);
            var updatedProduct = await _repository.Update(_mapper.Map<ProductEntity>(product), cancellationToken);

            return _mapper.Map<Product>(updatedProduct);
        }

        private static StockStatus CalculateStockStatus(int quantity, Category? category)
        {
            if (quantity > category!.LowStock) return StockStatus.InStock;
            if (quantity <= category.LowStock && quantity > category.OutOfStock) return StockStatus.LowStock;
            if (quantity <= category.OutOfStock) return StockStatus.OutOfStock;
            else return StockStatus.None;
        }
    }
}

