using Limupa.Discount.Context;
using Limupa.Discount.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// IdentityServer Configuration

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(config =>
{
    config.Authority = builder.Configuration["IdentityServerUrl"];
    config.Audience = "ResourceDiscount";
    config.RequireHttpsMetadata = false;
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContextPool<DiscountContext>(opt =>
{
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});


// Services Configuration
builder.Services.AddScoped<IDiscountCouponService, DiscountCouponService>();


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
