using DepresStore.Modules.Catalog.Application.Features.Products.Commands;
using DepresStore.Modules.Catalog.Application.Features.Products.Queries;
using DepresStore.Modules.Catalog.Composition;
using DepresStore.Modules.Inventory.Composition;
using DepresStore.Shared.Infrastructure;
using DepresStore.Shared.Kernel.Application;
using DepresStore.Shared.Kernel.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IEventBus, InProcessEventBus>();
builder.Services.AddScoped<IMediator, Mediator>();

// Add modules
builder.Services.AddCatalogModule(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddInventoryModule(builder.Configuration.GetConnectionString("DefaultConnection"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/products", async (IMediator mediator) =>
{
    var result = await mediator.SendAsync(new GetProductsQuery());
    return Results.Ok(result);
})
.WithName("GetAllProducts")
.WithOpenApi();

app.MapPost("/products", async (IMediator mediator) =>
{
    await mediator.SendAsync(new CreateProductCommand());
    return Results.Ok("CreateProductCommand sent");
})
.WithName("CreateProduct")
.WithOpenApi();

app.MapPut("/products", async (IMediator mediator) =>
{
    await mediator.SendAsync(new UpdateProductCommand());
    return Results.Ok("UpdateProductCommand sent");
})
.WithName("UpdateProduct")
.WithOpenApi();

app.Run();
