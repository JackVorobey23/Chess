using Microsoft.EntityFrameworkCore;

public class ChessDataBase : DbContext
{
    public ChessDataBase(DbContextOptions<ChessDataBase> options) : base(options)
    {
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        System.Console.WriteLine(string.Join(" ", new BoardDirector(new CommonBoardBuilder()).GetPieces()));
        modelBuilder.Entity<Player>().HasData
            (
                new Player(1, "Zhenyk", 1847),
                new Player(2, "Stepan", 3178),
                new Player(3, "Hikaru", 3023)
            );
        var rnd = new Random(123);
        Game game1 = new Game(1,1,2);
        game1.Board = new BoardDirector(new CommonBoardBuilder()).GetPieces();
        Game game2 = new Game(2,3,1);
        game1.Board = new BoardDirector(new CommonBoardBuilder()).GetPieces();

        modelBuilder.Entity<Game>().HasData
        (
            game1,
            game2
        );
    }
    public DbSet<Player> Players { get; set; }
    public DbSet<Game> Games { get; set; }
}