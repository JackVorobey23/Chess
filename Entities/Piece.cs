public class Piece
{
    public PieceName PieceName { get; set; }
    public string PiecePosition { get; set; }
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