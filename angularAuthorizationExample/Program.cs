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

builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = "https://dev-kadj1mmr6t5mt0pv.eu.auth0.com/";
                options.Audience = "kue1IOBTqYQM8xjqDRymGmYo74FWmro8"; //client id
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = $"https://dev-kadj1mmr6t5mt0pv.eu.auth0.com/",
                    ValidateAudience = true,
                    ValidateLifetime = true
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
//app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapFallbackToFile("index.html");;

app.Run();
