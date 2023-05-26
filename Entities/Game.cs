public class Game
{
    public int GameId { get; set; }
    public int WhitePlayerId { get; set; }
    public int BlackPlayerId { get; set; }
    public Player WhitePlayer { get; set; }
    public Player BlackPlayer { get; set; }

    public Game(int gameId, int whitePlayerId, int blackPlayerId, Player whitePlayer, Player blackPlayer)
    {
        GameId = gameId;
        WhitePlayerId = whitePlayerId;
        BlackPlayerId = blackPlayerId;
        WhitePlayer = whitePlayer;
        BlackPlayer = blackPlayer;
    }
}