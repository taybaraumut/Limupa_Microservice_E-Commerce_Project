using Limupa.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using Limupa.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
using Limupa.Order.Application.Interfaces;
using Limupa.Order.Application.Services;
using Limupa.Order.Persistence.Context;
using Limupa.Order.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// IdentityServer Configuration

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(config =>
{
    config.Authority = builder.Configuration["IdentityServerUrl"];
    config.Audience = "ResourceOrder";
    config.RequireHttpsMetadata = false;
});


// Services Configuration
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IOrderingRepository,OrderingRepository>();

// MediatR Configuration
builder.Services.AddApplicationService(builder.Configuration);

// CQRS Registiration
builder.Services.AddScoped<GetAddressByIdQueryHandler>();
builder.Services.AddScoped<GetAddressQueryHandler>();
builder.Services.AddScoped<CreateAddressCommandHandler>();
builder.Services.AddScoped<UpdateAddressCommandHandler>();
builder.Services.AddScoped<RemoveAddressCommandHandler>();

builder.Services.AddScoped<GetOrderDetailByIdQueryHandler>();
builder.Services.AddScoped<GetOrderDetailQueryHandler>();
builder.Services.AddScoped<CreateOrderDetailCommandHandler>();
builder.Services.AddScoped<UpdateOrderDetailCommandHandler>();
builder.Services.AddScoped<RemoveOrderDetailCommandHandler>();


// Context Configuration
builder.Services.AddDbContext<OrderContext>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
