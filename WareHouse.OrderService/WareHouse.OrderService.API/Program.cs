using FluentValidation;
using System.Text.Json.Serialization;
using WareHouse.OrderService.API.Mapper;
using WareHouse.OrderService.API.Middlewares;
using WareHouse.OrderService.API.Validators;
using WareHouse.OrderService.API.ViewModels;
using WareHouse.OrderService.Application.DI;
using WareHouse.OrderService.Infrastructure.DI;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var db = configuration.GetSection("MongoDbSettings");

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureDependencies();
builder.Services.AddApplicationDependencies();
builder.Services.AddAutoMapper(typeof(ModelViewModelProfile));
builder.Services.AddTransient<IValidator<PostOrderViewModel>, PostOrderViewModelValidator>()
                .AddTransient<IValidator<ChangeOrderStatusViewModel>, ChangeOrderStatusViewModelValidator>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
