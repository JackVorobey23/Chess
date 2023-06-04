using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ChessDataBase>((db) => db.UseInMemoryDatabase("Chess"));

builder.Services.AddScoped<IRepository<Player>, Repository<Player>>();
builder.Services.AddScoped<IRepository<Game>, Repository<Game>>();
builder.Services.AddSingleton<BlockingStrategyFactory>(_ => {

    BlockingStrategyFactory factory = new BlockingStrategyFactory();

    factory.RegisterStrategy(PieceName.Pawn, () => new PawnIsNotBlockingStrategy());
    factory.RegisterStrategy(PieceName.Knight, () => new KnightIsNotBlockingStrategy());
    factory.RegisterStrategy(PieceName.Bishop, () => new BishopIsNotBlockingStrategy());
    factory.RegisterStrategy(PieceName.Rook, () => new RookIsNotBlockingStrategy());
    factory.RegisterStrategy(PieceName.Queen, () => new QueenIsNotBlockingStrategy());
    factory.RegisterStrategy(PieceName.King, () => new KingIsNotBlockingStrategy());

    return factory;
});

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
