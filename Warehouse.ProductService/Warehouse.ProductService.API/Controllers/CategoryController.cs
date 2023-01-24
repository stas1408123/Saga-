using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Warehouse.ProductService.API.ViewModels;
using Warehouse.ProductService.Application.Contracts.Services;
using Warehouse.ProductService.Application.Models;

namespace Warehouse.ProductService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService service, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(service);
            ArgumentNullException.ThrowIfNull(mapper);

            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var models = await _service.Get(cancellationToken);

            return _mapper.Map<IEnumerable<CategoryViewModel>>(models);
        }

        [HttpGet("{id}")]
        public async Task<CategoryViewModel> GetById(int id, CancellationToken cancellationToken)
        {
            var model = await _service.GetById(id, cancellationToken);

            return _mapper.Map<CategoryViewModel>(model);
        }

        [HttpPost]
        public async Task<CategoryViewModel> Add([FromBody] PostCategoryViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Category>(viewModel);

            var result = await _service.Insert(model, cancellationToken);

            return _mapper.Map<CategoryViewModel>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _service.Delete(id, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<CategoryViewModel> Update(int id, [FromBody] UpdateCategoryViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Category>(viewModel);
            model.Id = id;

            var result = await _service.Update(model, cancellationToken);

            return _mapper.Map<CategoryViewModel>(result);
        }
    }
}
