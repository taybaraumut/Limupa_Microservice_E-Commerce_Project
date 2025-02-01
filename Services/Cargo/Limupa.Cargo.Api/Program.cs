using Limupa.Cargo.BusinessLayer.Abstract;
using Limupa.Cargo.BusinessLayer.Concrete;
using Limupa.Cargo.BusinessLayer.Mapping;
using Limupa.Cargo.DataAccessLayer.Abstract;
using Limupa.Cargo.DataAccessLayer.Concrete.Context;
using Limupa.Cargo.DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;


var builder = WebApplication.CreateBuilder(args);

// IdentityServer Configuration

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(config =>
{
    config.Authority = builder.Configuration["IdentityServerUrl"];
    config.Audience = "ResourceCargo";
    config.RequireHttpsMetadata = false;
});

// AutoMapper Configuration
builder.Services.AddAutoMapper(typeof(GeneralMapping));

// Services Configuration

builder.Services.AddScoped<ICargoCompanyDal, EfCargoCompanyDal>();
builder.Services.AddScoped<ICargoCompanyService,CargoCompanyManager>();

builder.Services.AddScoped<ICargoDetailDal, EfCargoDetailDal>();
builder.Services.AddScoped<ICargoDetailService,CargoDetailManager>();

builder.Services.AddScoped<ICargoCustomerDal,EfCargoCustomerDal>();
builder.Services.AddScoped<ICargoCustomerService, CargoCustomerManager>();

builder.Services.AddScoped<ICargoOperationDal, EfCargoOperationDal>();
builder.Services.AddScoped<ICargoOperationService, CargoOperationManager>();


// CargoContext Configuration

builder.Services.AddDbContext<CargoContext>();

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
