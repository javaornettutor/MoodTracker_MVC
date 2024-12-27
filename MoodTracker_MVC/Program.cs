using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MoodTracker_MVC.Models;
using Microsoft.Extensions.Hosting;
public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        // Access the environment after the host is built
        var environment = host.Services.GetRequiredService<IHostEnvironment>();

        // Configure environment-specific settings if needed
        if (environment.IsDevelopment())
        {
            // Custom development configuration can go here
        }

        // Run the application
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices(services =>
                {
                    // Add CORS policy
                    var allowAllOrigins = "CorsAllowAllOrigins";
                    services.AddCors(options =>
                    {
                        options.AddPolicy(allowAllOrigins,
                                          policy =>
                                          {
                                              policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                          });
                    });

                    // Add Swagger
                    services.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new OpenApiInfo
                        {
                            Title = "Mood Tracker",
                            Version = "v1",
                            Description = "Mood Tracker ASP.NET Core Web API"
                        });
                    });

                    // Add controllers and views
                    services.AddControllersWithViews();

                    // Add DbContext
                    services.AddDbContext<MoodTracker2Context>(options =>
                        options.UseSqlServer(services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection")));
                });

                webBuilder.Configure(app =>
                {
                    // Configure the HTTP request pipeline
                    
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    });

                    app.UseStaticFiles();

                    app.UseRouting();

                    app.UseAuthorization();
                    app.UseCors("CorsAllowAllOrigins");

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllerRoute(
                            name: "default",
                            pattern: "{controller=Home}/{action=Index}/{id?}");
                    });
                   
                });
            });
}
