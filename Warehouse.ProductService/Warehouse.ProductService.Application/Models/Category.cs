﻿namespace Warehouse.ProductService.Application.Models
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int LowStock { get; set; }
        public int OutOfStock { get; set; }

        List<Product>? Products = new();
    }
}
