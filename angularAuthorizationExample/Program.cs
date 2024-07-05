using angularAuthorizationExample.Abstract;
using angularAuthorizationExample.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddIdentityServer()
//    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();


builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "https://login.microsoftonline.com/e9d9b795-b2a5-435c-97c9-77a382765404";
                options.Audience = "api://6ced2e7e-d6b4-4a5d-8f0c-57c70a9b2c8d"; //client id
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = false                    
                };
            });
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
//builder.Services.AddAuthorization(config =>
//{
//    config.AddPolicy("AuthZPolicy", policyBuilder =>
//        policyBuilder.Requirements.Add(new ScopeAuthorizationRequirement() { RequiredScopesConfigurationKey = $"AzureAd:Scopes" }));
//});

//builder.Services.AddAuthorization();


builder.Services.AddRazorPages();

builder.Services.AddOptions();
var firebirdConnectionString = builder.Configuration.GetConnectionString("NaprawyConnection");
builder.Services.AddSingleton<INaprawyDbStorage>(x => new NaprawyDbStorage(firebirdConnectionString));
//https://stackoverflow.com/questions/53884417/net-core-di-ways-of-passing-parameters-to-constructor
var app = builder.Build();
//Ustawienie certyfikat�w:
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
//app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapFallbackToFile("index.html");;

app.Run();
