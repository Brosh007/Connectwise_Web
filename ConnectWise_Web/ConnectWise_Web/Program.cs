using ConnectWise_Web;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.SignalR;
using ConnectWise_Web.Models;

using Microsoft.AspNetCore.Http;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("FirebaseConfig/connectxwise-firebase-adminsdk-bqn4w-6082b41e5d.json"),
});
// Add Bologic as a Singleton service
builder.Services.AddSingleton<Bologic>(_ => new Bologic(connectionString, _.GetRequiredService<IHttpContextAccessor>()));
builder.Services.AddSingleton<INLogic>(_ => new INLogic(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession(); // Add this line to enable session middleware

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Register}/{action=Index}/{id?}");

app.Run();