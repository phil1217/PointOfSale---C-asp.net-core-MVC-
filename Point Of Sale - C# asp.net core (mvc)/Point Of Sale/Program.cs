using Microsoft.EntityFrameworkCore;
using Point_Of_Sale.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "CustomersRoute",
    pattern: "Customers/{action}/{id?}",
    defaults: new { controller = "Customers" });

app.MapControllerRoute(
    name: "EmployeesRoute",
    pattern: "Employees/{action}/{id?}",
    defaults: new { controller = "Employees" });

app.MapControllerRoute(
    name: "ProductsRoute",
    pattern: "Products/{action}/{id?}",
    defaults: new { controller = "Products" });

app.MapControllerRoute(
    name: "ReportsRoute",
    pattern: "Reports/{action}/{id?}",
    defaults: new { controller = "Reports" });

app.MapControllerRoute(
    name: "SalesRoute",
    pattern: "Sales/{action}/{id?}",
    defaults: new { controller = "Sales" });

app.MapControllerRoute(
    name: "TaxRoute",
    pattern: "Tax/{action}/{id?}",
    defaults: new { controller = "Tax" });

app.MapControllerRoute(
    name: "VendorsRoute",
    pattern: "Vendors/{action}/{id?}",
    defaults: new { controller = "Vendors" });

app.Run();
