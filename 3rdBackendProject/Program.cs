using _3rdBackendProject.DAL;
using _3rdBackendProject.Models;
using _3rdBackendProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 8;
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;
    opt.User.RequireUniqueEmail = true; 
    opt.Lockout.AllowedForNewUsers = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    opt.Lockout.MaxFailedAccessAttempts = 3;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();



builder.Services.AddControllersWithViews();
builder.Services.AddScoped<LayoutService>();
builder.Services.AddSession(opt => { opt.IdleTimeout = TimeSpan.FromMinutes(5); });
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDbContext<AppDbContext>(opt =>
         opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();
app.UseRouting();
app.UseSession();
app.UseStaticFiles();



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=Home}/{action=index}"
    );



app.Run();
