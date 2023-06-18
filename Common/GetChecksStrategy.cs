public interface IGetChecksStrategy
{
    Piece IsKingUnderCheck(List<Piece> board, PieceColor kingColor, BlockingStrategyFactory factory);
}

public class GetDiagonalCheck : IGetChecksStrategy
{
    public Piece IsKingUnderCheck(List<Piece> board, PieceColor kingColor, BlockingStrategyFactory factory)
    {
        MoveAllowed moveAllowed = new MoveAllowed(board, factory);
        System.Console.WriteLine("Board for check if king under check: ");
        System.Console.WriteLine(String.Join(" ", board.Select(p => $"{p.PieceName} - {p.PiecePosition}")));
        string kingPosition = board.Find(p => p.PieceName == PieceName.King && p.PieceColor == kingColor).PiecePosition;

        System.Console.WriteLine("King position: " + kingPosition);

        List<Piece> piecesToCheck = board
            .Where(p => p.PieceColor != kingColor && p.PieceName != PieceName.King)
            .Where(p => p.PieceName == PieceName.Bishop || p.PieceName == PieceName.Queen || p.PieceName == PieceName.Pawn)
            .ToList();
        System.Console.WriteLine($"Pieces to check: {String.Join(';', piecesToCheck.Select(p => p.PieceName + p.PiecePosition))}");
        foreach (Piece piece in piecesToCheck)
        {
            if(moveAllowed.CheckAllowedRequest(piece, kingPosition))
            {
                System.Console.WriteLine($"move allowed: {piece.PieceName}: {piece.PiecePosition}-{kingPosition}");
                
                System.Console.WriteLine($"Piece is not blocking? ({piece.PieceName}, {piece.PiecePosition} - {kingPosition})");
                if (factory[piece.PieceName].PieceIsNotBlocking(piece, kingPosition, board))
                {
                    System.Console.WriteLine($"piece not blocking: {piece}; {kingPosition}");
                    return piece;
                }
            }
        }
        return null;
    }

}
public class GetLineCheck : IGetChecksStrategy
{
    public Piece IsKingUnderCheck(List<Piece> board, PieceColor kingColor, BlockingStrategyFactory factory)
    {
        MoveAllowed moveAllowed = new MoveAllowed(board, factory);
        string kingPosition = board.Find(p => p.PieceName == PieceName.King).PiecePosition;

        List<Piece> piecesToCheck = board
            .Where(p => p.PieceColor != kingColor && p.PieceName != PieceName.King)
            .Where(p => p.PieceName == PieceName.Rook || p.PieceName == PieceName.Queen)
            .ToList();
        
        foreach (Piece piece in piecesToCheck)
        {
            if(moveAllowed.CheckAllowedRequest(piece, kingPosition))
            {
                System.Console.WriteLine($"move allowed: {piece.PieceName}: {piece.PiecePosition}-{kingPosition}");
                
                if (factory[piece.PieceName].PieceIsNotBlocking(piece, kingPosition, board))
                {
                    System.Console.WriteLine($"piece noot blocking: {piece}; {kingPosition}");
                    return piece;
                }
            }
        }
        return null;
    }
}
public class GetKnightCheck : IGetChecksStrategy
{
    public Piece IsKingUnderCheck(List<Piece> board, PieceColor kingColor, BlockingStrategyFactory factory)
    {
        MoveAllowed moveAllowed = new MoveAllowed(board, factory);
        string kingPosition = board.Find(p => p.PieceName == PieceName.King).PiecePosition;

        List<Piece> piecesToCheck = board
            .Where(p => p.PieceColor != kingColor && p.PieceName != PieceName.King)
            .Where(p => p.PieceName == PieceName.Knight)
            .ToList();
        
        foreach (Piece piece in piecesToCheck)
        {
            if(moveAllowed.CheckAllowedRequest(piece, kingPosition))
            {
                System.Console.WriteLine($"move allowed: {piece.PieceName}: {piece.PiecePosition}-{kingPosition}");
                
                if (factory[piece.PieceName].PieceIsNotBlocking(piece, kingPosition, board))
                {
                    System.Console.WriteLine($"piece not blocking: {piece}; {kingPosition}");
                    return piece;
                }
            }
        }
        return null;
    }
}