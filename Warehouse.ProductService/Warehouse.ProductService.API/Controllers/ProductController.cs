using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Warehouse.ProductService.API.ViewModels;
using Warehouse.ProductService.Application.Contracts.Services;
using Warehouse.ProductService.Application.Models;

namespace Warehouse.ProductService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductController(IProductService service, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(service);
            ArgumentNullException.ThrowIfNull(mapper);

            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var models = await _service.Get(cancellationToken);

            return _mapper.Map<IEnumerable<ProductViewModel>>(models);
        }

        [HttpGet("{id}")]
        public async Task<ProductViewModel> GetById(int id, CancellationToken cancellationToken)
        {
            var model = await _service.GetById(id, cancellationToken);

            return _mapper.Map<ProductViewModel>(model);
        }

        [HttpPost]
        public async Task<ProductViewModel> Add([FromBody] PostProductViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Product>(viewModel);

            var result = await _service.Insert(model, cancellationToken);

            return _mapper.Map<ProductViewModel>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _service.Delete(id, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<ProductViewModel> Update(int id, [FromBody] UpdateProductViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Product>(viewModel);
            model.Id = id;

            var result = await _service.Update(model, cancellationToken);

            return _mapper.Map<ProductViewModel>(result);
        }
    }
}
