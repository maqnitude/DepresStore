using DepresStore.Modules.Catalog.Application.Features.Products.Commands;
using DepresStore.Modules.Catalog.Application.Features.Products.DomainEventHandlers;
using DepresStore.Modules.Catalog.Application.Features.Products.Queries;
using DepresStore.Modules.Catalog.Core.DomainEvents;
using DepresStore.Modules.Inventory.Application.Features.Products.IntegrationEventHandlers;
using DepresStore.Shared.Infrastructure;
using DepresStore.Shared.Kernel.Cqrs;
using DepresStore.Shared.Kernel.EventBus;
using DepresStore.Shared.Kernel.IntegrationEvents;
using DepresStore.Shared.Kernel.Mediator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddSingleton<IEventBus, InProcessEventBusReflectionBased>();
builder.Services.AddSingleton<IEventBus, InProcessEventBus>();
builder.Services.AddScoped<IMediator, Mediator>();

// Add query request handlers
builder.Services.AddScoped<IQueryHandler<GetProductsQuery, PaginatedList<ProductDto>>, GetProductsQueryHandler>();

// Add command request handlers
builder.Services.AddScoped<ICommandHandler<CreateProductCommand>, CreateProductCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateProductCommand>, UpdateProductCommandHandler>();

// Add domain event handlers
builder.Services.AddScoped<IEventHandler<ProductNameChanged>, ProductNameChangedEventHandler>();

// Add integration event handlers
builder.Services.AddScoped<IEventHandler<ProductCreated>, ProductCreatedEventHandler>();

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
