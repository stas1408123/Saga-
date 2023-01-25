using AutoMapper;
using Warehouse.ProductService.Application.Contracts.Services;
using Warehouse.ProductService.Application.Models;
using WarehouseService.Application.Contracts.Repositories;
using WarehouseService.Domain.Entities;

namespace Warehouse.ProductService.Application.Services
{
    public class CategoryService : GenericService<Category, CategoryEntity>, ICategoryService
    {
        public CategoryService(IGenericRepository<CategoryEntity> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
