using System.ComponentModel.DataAnnotations.Schema;

public class Game
{
    public int GameId { get; set; }
    public int WhitePlayerId { get; set; }
    public int BlackPlayerId { get; set; }
    public string Moves { get; set; }
    public string Board { get; set; }
    public PieceColor? WinnerColor { get; set; }
    public Game(int gameId, int whitePlayerId, int blackPlayerId, string board)
    {
        GameId = gameId;
        WhitePlayerId = whitePlayerId;
        BlackPlayerId = blackPlayerId;
        Board = board;
    }
}