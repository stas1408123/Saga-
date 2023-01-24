using AutoMapper;
using Warehouse.ProductService.Application.Contracts.Repositories;
using Warehouse.ProductService.Application.Contracts.Services;
using Warehouse.ProductService.Application.Models;
using WarehouseService.Domain.Entities;

namespace Warehouse.ProductService.Application.Services
{
    public class ProductService : GenericService<Product, ProductEntity>, IProductService
    {
        public ProductService(IProductRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
