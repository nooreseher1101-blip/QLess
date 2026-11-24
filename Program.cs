using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QLessProject.Data;
using QLessProject.Repositories.Interfaces;
using QLessProject.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<ICounterRepository, CounterRepository>();

builder.Services.AddScoped<IQueueTicketRepository, QueueTicketRepository>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Identity with Roles
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

// MVC and Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Create roles and assign initial Admin
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    // Create default roles if they do not exist
    string[] roles = { "User", "Staff", "Admin" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Initial Admin email
    string initialAdminEmail = "nooreseher1101@gmail.com";

    var adminUser = await userManager.FindByEmailAsync(initialAdminEmail);

    if (adminUser != null)
    {
        var currentRoles = await userManager.GetRolesAsync(adminUser);

        // Remove other roles from initial Admin
        foreach (var role in currentRoles)
        {
            if (role != "Admin")
            {
                await userManager.RemoveFromRoleAsync(adminUser, role);
            }
        }

        // Assign Admin role
        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

// Configure HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// MVC Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Identity Razor Pages
app.MapRazorPages();

app.Run();