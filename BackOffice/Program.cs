using Domain.Services.Products;
using Domain.Services.Users;
using Service.Products;
using Service.Products.Repository;
using Service.Users;
using Service.Users.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();

builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddScoped<UsersRepository>();

builder.Services.AddScoped<EmailService>();

builder.Services.AddScoped<IProductsService, ProductService>();

builder.Services.AddScoped<ProductsRepository>();

var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin()
    .AllowAnyMethod().AllowAnyHeader());

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();