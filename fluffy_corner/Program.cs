using fluffy_corner.Data;
using fluffy_corner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// add identity services with custom user and role, and configure password and lockout settings
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
    // 1. (Password Validation)

    options.Password.RequireDigit = true;            // يجب أن تحتوي على رقم
    options.Password.RequiredLength = 8;             // الحد الأدنى للطول 8 حروف
    options.Password.RequireNonAlphanumeric = true;  // يجب أن تحتوي على رمز خاص (@, #, !)
    options.Password.RequireUppercase = true;        // يجب أن تحتوي على حرف كبير
    options.Password.RequireLowercase = true;        // يجب أن تحتوي على حرف صغير

    // 2. (Sign-In Validation)
    options.SignIn.RequireConfirmedAccount = false;  
    options.User.RequireUniqueEmail = true;          // منع تسجيل أكثر من حساب بنفس الإيميل

    // 3.  (Lockout) - مهمة جداً لـ Error Handling
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // قفل الحساب لمدة 5 دقائق
    options.Lockout.MaxFailedAccessAttempts = 5;      // قفل الحساب بعد 5 محاولات خاطئة
    options.Lockout.AllowedForNewUsers = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
// Seed roles and default admin user on application startup

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbSeeder.SeedRolesAndAdminAsync(services);
}
app.Run();