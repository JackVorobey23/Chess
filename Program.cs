using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ChessDataBase>((db) => db.UseInMemoryDatabase("Chess"));

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAllHeaders", 
    builder => 
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSignalR(options => {
    options.EnableDetailedErrors = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints => 
{
    endpoints.MapControllers();
    endpoints.MapHub<GameHub>("/chess");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
