using DepresStore.Modules.Catalog.Application.Features.CreateProduct;
using DepresStore.Modules.Catalog.Application.Features.GetAllProducts;
using DepresStore.Modules.Catalog.Application.Features.UpdateProduct;
using DepresStore.Modules.Catalog.Core.ProductAggregate.Events;
using DepresStore.Shared.Infrastructure;
using DepresStore.Shared.Kernel.Cqrs;
using DepresStore.Shared.Kernel.EventBus;
using DepresStore.Shared.Kernel.Mediator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IEventBus, InProcessEventBus>();
builder.Services.AddScoped<IMediator, Mediator>();

// Add query request handlers
builder.Services.AddScoped<IQueryHandler<GetAllProductsQuery, PaginatedList<ProductDto>>, GetAllProductsQueryHandler>();

// Add command request handlers
builder.Services.AddScoped<ICommandHandler<CreateProductCommand>, CreateProductCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateProductCommand>, UpdateProductCommandHandler>();

// Add domain event handlers
builder.Services.AddScoped<IEventHandler<ProductNameChangedEvent>, ProductNameChangedEventHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

// Subscribe domain event handlers here for now
var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<ProductNameChangedEvent, ProductNameChangedEventHandler>();

app.MapGet("/products", async (IMediator mediator) =>
{
    var result = await mediator.SendAsync(new GetAllProductsQuery());
    return Results.Ok(result);
});

app.MapPost("/products", async (IMediator mediator) =>
{
    await mediator.SendAsync(new CreateProductCommand());
    return Results.Ok("CreateProductCommand sent");
});

app.MapPut("/products", async (IMediator mediator) =>
{
    await mediator.SendAsync(new UpdateProductCommand());
    return Results.Ok("UpdateProductCommand sent");
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
