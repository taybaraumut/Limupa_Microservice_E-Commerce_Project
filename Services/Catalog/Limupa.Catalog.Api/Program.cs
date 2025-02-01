using Limupa.Catalog.Api.CloudStorage.ConfigOptions;
using Limupa.Catalog.Api.Services.AboutServices;
using Limupa.Catalog.Api.Services.ContactServices;
using Limupa.Catalog.Api.Services.FeatureServices;
using Limupa.Catalog.Api.Services.FeatureSliderServices;
using Limupa.Catalog.Api.Services.GoogleCloudStorageServices;
using Limupa.Catalog.Api.Services.SpecialOfferServices;
using Limupa.Catalog.Services.CategoryServices;
using Limupa.Catalog.Services.ProductDetailServices;
using Limupa.Catalog.Services.ProductImageServices;
using Limupa.Catalog.Services.ProductServices;
using Limupa.Catalog.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// IdentityServer Configuration

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(config =>
{
    config.Authority = builder.Configuration["IdentityServerUrl"];
    config.Audience = "ResourceCatalog";
    config.RequireHttpsMetadata = false;
});

// Services Configuration
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductDetailService, ProductDetailService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFeatureSliderService, FeatureSliderService>();
builder.Services.AddScoped<ISpecialOfferService, SpecialOfferService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<IContactService, ContactService>();

builder.Services.Configure<GCSConfigOptions>(builder.Configuration);
builder.Services.AddSingleton<ICloudStorageService, CloudStorageService>();


// Database Configuration
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddScoped<IDatabaseSettings>(config =>
{
    return config.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

// AutoMapper Configuration
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

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
