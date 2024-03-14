using Microsoft.EntityFrameworkCore;
using WebApplication8.Data;
using WebApplication8.Models;
using Newtonsoft;
var builder = WebApplication.CreateBuilder(args);



//builder.Services.AddTransient<IEmailServices, EmailSender>();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ServerLink")));
// Adding Session
builder.Services.AddSession();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1);
    options.Cookie.Name = "LearnNext";
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


// Adding session 
app.UseCookiePolicy();
app.UseStaticFiles();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Chennel}/{action=Index}/{id?}");
app.Run();
