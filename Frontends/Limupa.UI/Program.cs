using Limupa.UI.Handlers;
using Limupa.UI.Services.BasketServices;
using Limupa.UI.Services.CargoServices.CargoCompanyServices;
using Limupa.UI.Services.CatalogServices.AboutServices;
using Limupa.UI.Services.CatalogServices.CategoryServices;
using Limupa.UI.Services.CatalogServices.FeatureServices;
using Limupa.UI.Services.CatalogServices.FeatureSliderServices;
using Limupa.UI.Services.CatalogServices.ProductDetailService;
using Limupa.UI.Services.CatalogServices.ProductImageServices;
using Limupa.UI.Services.CatalogServices.ProductServices;
using Limupa.UI.Services.CatalogServices.SpecialOfferServices;
using Limupa.UI.Services.CommentServices.UserCommentServices;
using Limupa.UI.Services.Concrete;
using Limupa.UI.Services.DiscountServices;
using Limupa.UI.Services.Interfaces;
using Limupa.UI.Services.OrderServices.OrderAddressServices;
using Limupa.UI.Services.OrderServices.OrderOrderingServices;
using Limupa.UI.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Login/Index";
    opt.LogoutPath = "/Login/Index";
    opt.AccessDeniedPath = "/Login/Index";
    opt.Cookie.HttpOnly = true;
    opt.Cookie.SameSite = SameSiteMode.Strict;
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    opt.Cookie.Name = "limupashopjwt";
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Login/Index";
    opt.ExpireTimeSpan = TimeSpan.FromDays(5);
    opt.Cookie.Name = "LimupaCookie";
    opt.SlidingExpiration = true;
});


builder.Services.AddAccessTokenManagement();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAccountLoginService, AccountLoginService>();

builder.Services.AddHttpClient<IIdentityService, IdentityService>();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));

builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddScoped<ClientCredentialTokenHandler>();
builder.Services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();

var values = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
builder.Services.AddHttpClient<IUserService, UserService>(opt =>
{
    opt.BaseAddress = new Uri(values!.IdentityUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();


builder.Services.AddHttpClient<ICategoryService, CategoryService>(opt =>
{
    opt.BaseAddress = new Uri($"{values!.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IProductService, ProductService>(opt =>
{
    opt.BaseAddress = new Uri($"{values!.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<ISpecialOfferService, SpecialOfferService>(opt =>
{
    opt.BaseAddress = new Uri($"{values!.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IFeatureSliderService, FeatureSliderService>(opt =>
{
    opt.BaseAddress = new Uri($"{values!.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IFeatureService, FeatureService>(opt =>
{
    opt.BaseAddress = new Uri($"{values!.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IAboutService, AboutService>(opt =>
{
    opt.BaseAddress = new Uri($"{values!.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IProductImageService, ProductImageService>(opt =>
{
    opt.BaseAddress = new Uri($"{values!.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IProductDetailService, ProductDetailService>(opt =>
{
    opt.BaseAddress = new Uri($"{values!.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IUserCommentService, UserCommentService>(opt =>
{
    opt.BaseAddress = new Uri($"{values!.OcelotUrl}/{values.Comment.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IDiscountService, DiscountService>(opt =>
{
    opt.BaseAddress = new Uri($"{values!.OcelotUrl}/{values.Discount.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IBasketService, BasketService>(opt =>
{
    opt.BaseAddress = new Uri($"{values!.OcelotUrl}/{values.Basket.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>().AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IOrderAddressService, OrderAddressService>(opt =>
{
    opt.BaseAddress = new Uri($"{values!.OcelotUrl}/{values.Order.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IOrderOrderingService, OrderOrderingService>(opt =>
{
    opt.BaseAddress = new Uri($"{values!.OcelotUrl}/{values.Order.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<ICargoCompanyService, CargoCompanyService>(opt =>
{
    opt.BaseAddress = new Uri($"{values!.OcelotUrl}/{values.Cargo.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();


builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);


app.MapControllerRoute(
    name: "about",
    pattern: "about.html",
    defaults: new { controller = "About", action = "Index" }
);

app.MapControllerRoute(
    name: "product",
    pattern: "product-detail/{id}.html",
    defaults: new { controller = "ProductDetail", action = "Index" }
);

app.MapControllerRoute(
    name: "contact",
    pattern: "contact.html",
    defaults: new { controller = "Contact", action = "Index" }
);

app.MapControllerRoute(
    name: "account",
    pattern: "login.html",
    defaults: new { controller = "Account", action = "Login" }
);

app.MapControllerRoute(
    name: "account",
    pattern: "register.html",
    defaults: new { controller = "Account", action = "Register" }
);

app.MapControllerRoute(
    name: "profile",
    pattern: "{Name}-user-profile.html",
    defaults: new { controller = "Profile", action = "Index" }
);

app.MapControllerRoute(
    name: "productbluetoothlist",
    pattern: "product-bluetooth-list.html",
    defaults: new { controller = "ProductBluetoothList", action = "Index" }
);

app.MapControllerRoute(
    name: "order",
    pattern: "order.html",
    defaults: new { controller = "Order", action = "Index" }
);

app.MapControllerRoute(
    name: "shoppingcart",
    pattern: "shopping-cart.html",
    defaults: new { controller = "ShoppingCart", action = "Index" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");


app.Run();
