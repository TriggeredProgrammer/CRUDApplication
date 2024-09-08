using CRUDApplication.Areas.Identity.Data;
using CRUDApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Retrieve connection strings from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection")
    ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");
var customerDbConnectionString = builder.Configuration.GetConnectionString("dbcs");

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register ApplicationDbContext to handle Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register CustomerDbContext for your Customer data
builder.Services.AddDbContext<CustomerDbContext>(options =>
    options.UseSqlServer(customerDbConnectionString));

// Register Identity services and ApplicationUser
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>() // Add role management
.AddEntityFrameworkStores<ApplicationDbContext>() // Use ApplicationDbContext for Identity
.AddDefaultTokenProviders(); // Enable default token providers

// Build the application
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Show detailed error page in development
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Show user-friendly error page in production
    app.UseHsts(); // Use HSTS in production
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Ensure Identity Authentication is in the middleware pipeline
app.UseAuthorization();

// Configure the default route for the MVC controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
// Run the application
app.Run();
