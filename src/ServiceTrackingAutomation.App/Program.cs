using ServiceTrackingAutomation.Application.Manager;
using ServiceTrackingAutomation.Domain.Abstract;
using ServiceTrackingAutomation.Domain.Entities;
using ServiceTrackingAutomation.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region  Session

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(720);
    options.Cookie.HttpOnly = true;
    // Make the session cookie essential
    options.Cookie.IsEssential = true;
});
builder.Services.AddMemoryCache();
builder.Services.AddDataProtection();
builder.Services.AddDistributedMemoryCache();

#endregion



builder.Services.AddDbContext<BusinessDbContext>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();


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

app.UseSession();
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
