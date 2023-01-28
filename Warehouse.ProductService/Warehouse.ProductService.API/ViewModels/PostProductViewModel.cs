namespace Warehouse.ProductService.API.ViewModels
{
    public class PostProductViewModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
    }
}
