#region Register DI dependencies

using Microsoft.Net.Http.Headers;

using Microsoft.EntityFrameworkCore;
using ProjektMitAPI.Models;
using ProjektMitAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjektMitAPI.Controllers;
using System.Text;
var builder = WebApplication.CreateBuilder(args); 
builder.Services.AddRazorPages(); 
builder.Services.AddControllers(); 
builder.Services.AddDbContext<MovieAppContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<DbInitializer>();

// Needed for API access through the JS client; see UseCors()
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS_CONFIG", cors =>
    {
        cors.WithMethods("*")
            .WithHeaders("*")
            .WithOrigins("*");
    });
});

// Add for JWT mappings
builder.Services 
    .AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o => 
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = JwtConfiguration.ValidIssuer,
            ValidAudience = JwtConfiguration.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(JwtConfiguration.IssuerSigningKey)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
       };
 });

#endregion


#region Application Startup

var app = builder.Build();

// Add for JWT authorization (middleware)
app.UseAuthentication();
app.UseAuthorization();

// Add for cshtml page mappings
app.MapRazorPages();
app.MapControllers();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = (context) =>
    {
        var headers = context.Context.Response.GetTypedHeaders();

        headers.CacheControl = new CacheControlHeaderValue
        {
            NoCache = true,
            NoStore = true
        };
    }
});

app.UseCors("CORS_CONFIG");


using (var scope = app.Services.CreateScope()) // oberhalb von app.Run()
{
    scope.ServiceProvider.GetRequiredService<DbInitializer>().Run(); // run initializer
}

// startup server
app.Run();

#endregion
