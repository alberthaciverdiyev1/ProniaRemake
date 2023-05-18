using _3rdBackendProject.DAL;
using _3rdBackendProject.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<LayoutService>();

builder.Services.AddDbContext<AppDbContext>(opt =>
         opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();
app.UseRouting();   

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
