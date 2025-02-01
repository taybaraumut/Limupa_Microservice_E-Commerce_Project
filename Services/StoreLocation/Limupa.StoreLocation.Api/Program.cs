using Limupa.StoreLocation.Api;
using Limupa.StoreLocation.Api.Context;
using Limupa.StoreLocation.Api.Extensions;
using Limupa.StoreLocation.Api.Services;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<JsonOptions>(options => options.SerializerOptions.TypeInfoResolver = StoreLocationJsonSerializerContext.Default);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStoreLocationService, StoreLocationService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<StoreLocationContext>(opt =>
{
    opt.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseBadwordHandler();
app.UseHttpsRedirection();
app.MapControllers();
app.MapGet("/storelocationlist", (StoreLocationContext context) => StoreLocationContext.GetAllStoreLocationAsync(context));
app.Run();
