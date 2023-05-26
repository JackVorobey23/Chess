public class Game
{
    public int GameId { get; set; }
    public int WhitePlayerId { get; set; }
    public int BlackPlayerId { get; set; }
    public Player WhitePlayer { get; set; }
    public Player BlackPlayer { get; set; }
    public string Moves { get; set; }

    public Game(int gameId, int whitePlayerId, int blackPlayerId)
    {
        GameId = gameId;
        WhitePlayerId = whitePlayerId;
        BlackPlayerId = blackPlayerId;
    }
}