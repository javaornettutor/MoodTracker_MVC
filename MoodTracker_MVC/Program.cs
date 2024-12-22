using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MoodTracker_MVC.Models;

var builder = WebApplication.CreateBuilder(args);
var allowAllOrigins = "CorsAllowAllOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowAllOrigins,
                          policy =>
                          {
                              policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                          });
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mood Tracker",
        Version = "v1",
        Description = "Mood Tracker ASP.NET Core Web API"
    });
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MoodTracker2Context>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseCors("CorsAllowAllOrigins");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
