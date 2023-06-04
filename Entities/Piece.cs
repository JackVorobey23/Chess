using System.Text.Json.Serialization;

public class Piece
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PieceName PieceName { get; set; }
    
    public string PiecePosition { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PieceColor PieceColor { get; set; }

    public Piece(PieceName pieceName, string piecePosition, PieceColor pieceColor)
    {
        PieceName = pieceName;
        PiecePosition = piecePosition;
        PieceColor = pieceColor;
    }
}

public enum PieceName {
    Pawn,
    Knight, 
    Bishop,
    Rook,
    Queen,
    King
}
public enum PieceColor{
    White,
    Black
}