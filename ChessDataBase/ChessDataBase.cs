using Microsoft.EntityFrameworkCore;

public class ChessDataBase : DbContext
{
    public ChessDataBase(DbContextOptions<ChessDataBase> options) : base(options)
    {
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Player>().HasData
            (
                new Player(1, "Zhenyk", 1847),
                new Player(2, "Stepan", 3178),
                new Player(3, "Hikaru", 3023)
            );
        var rnd = new Random(123);
        modelBuilder.Entity<Game>().HasData
        (
            new Game(1, 1, 2),
            new Game(2, 3, 1)
        );
    }
    public DbSet<Player> Players { get; set; }
    public DbSet<Game> Games { get; set; }
}