using System.Text.Json.Serialization;
using Warehouse.ProductService.API.Mapper;
using Warehouse.ProductService.Application.DI;
using WarehouseService.Infrastructure.Persistence.DI;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceDependencies(configuration);
builder.Services.AddApplicationDependencies();

builder.Services.AddAutoMapper(typeof(ModelViewModelProfile));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
