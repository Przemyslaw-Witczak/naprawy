using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using angularAuthorizationExample.Data;
using angularAuthorizationExample.Models;
using Microsoft.Extensions.DependencyInjection;
using angularAuthorizationExample.Abstract;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddOptions();
//builder.Services.AddSingleton<INaprawyDbStorage, NaprawyDbStorage>().AddOptions<string>("connectionString");
var firebirdConnectionString = builder.Configuration.GetConnectionString("NaprawyConnection");
builder.Services.AddSingleton<INaprawyDbStorage>(x => new NaprawyDbStorage(firebirdConnectionString));
//https://stackoverflow.com/questions/53884417/net-core-di-ways-of-passing-parameters-to-constructor
var app = builder.Build();
//Ustawienie certyfikatów:
//https://stackoverflow.com/questions/64186403/blazor-web-assembly-app-net-core-hosted-publish-runtime-error/66448397#66448397
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapFallbackToFile("index.html");;

app.Run();
