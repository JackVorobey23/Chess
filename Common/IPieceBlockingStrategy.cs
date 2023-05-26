public interface IPieceBlockingStrategy
{
    bool PieceIsNotBlocking(Piece piece, string move, List<Piece> board);
}
public class PawnIsNotBlockingStrategy : IPieceBlockingStrategy
{
    public bool PieceIsNotBlocking(Piece piece, string move, List<Piece> board)
    {
        if (piece.PiecePosition[0] != move[0])
        {
            if (board.Find(p => p.PiecePosition == move && p.PieceColor == piece.PieceColor) is not null)
            {
                return false;
            }
            return true;
        }
        else
        {
            var condition = (int i) => true;

            condition = piece.PiecePosition[1] < move[1] ? (i) => i <= move[1] : (i) => i >= move[1];

            for (int i = piece.PiecePosition[1] + 1; condition(i); i++)
            {
                if (board.Find(p => p.PiecePosition[1] == i) is not null)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
public class KnightIsNotBlockingStrategy : IPieceBlockingStrategy
{
    public bool PieceIsNotBlocking(Piece piece, string move, List<Piece> board)
    {
        if (board.Find(p => p.PieceColor == piece.PieceColor && p.PiecePosition == move) is not null)
        {
            return false;
        }
        return true;
    }
}
public class RookIsNotBlockingStrategy : IPieceBlockingStrategy
{
    public bool PieceIsNotBlocking(Piece piece, string move, List<Piece> board)
    {
        var condition = (int i) => true;

        if (piece.PiecePosition[1] != move[1])
        {
            condition = piece.PiecePosition[1] < move[1] ? (i) => i <= move[1] : (i) => i >= move[1];

            for (int i = piece.PiecePosition[1] + 1; condition(i); i++)
            {
                Piece blockingPiece = board.Find(p => p.PiecePosition[1] == i);
                if (board.Find(p => p.PiecePosition[1] == i) is not null)
                {
                    if (i == move[1] && blockingPiece.PieceColor != piece.PieceColor)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }
        else
        {
            condition = piece.PiecePosition[0] < move[0] ? (i) => i <= move[0] : (i) => i >= move[0];

            for (int i = piece.PiecePosition[0] + 1; condition(i); i++)
            {
                Piece blockingPiece = board.Find(p => p.PiecePosition[0] == i);

                if (blockingPiece is not null)
                {
                    if (i == move[0] && blockingPiece.PieceColor != piece.PieceColor)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }
        return true;
    }
}
public class BishopIsNotBlockingStrategy : IPieceBlockingStrategy
{
    public bool PieceIsNotBlocking(Piece piece, string move, List<Piece> board)
    {
        var conditionI = (int i) => true;
        var conditionJ = (int j) => true;

        conditionI = piece.PiecePosition[0] < move[0] ? (i) => i <= move[0] : (i) => i >= move[0];

        conditionJ = piece.PiecePosition[1] < move[1] ? (j) => j <= move[1] : (j) => j >= move[1];

        for (int i = piece.PiecePosition[0] + 1; conditionI(i); i++)
        {
            for (int j = piece.PiecePosition[1] + 1; conditionJ(j); j++)
            {
                Piece blockingPiece = board.Find(p => p.PiecePosition[0] == i && p.PiecePosition[1] == j);

                if (blockingPiece is not null)
                {
                    if (blockingPiece.PiecePosition == move && piece.PieceColor != blockingPiece.PieceColor)
                    {
                        continue;
                    }
                    return false;
                }
            }
        }
        return true;
    }
}
public class QueenIsNotBlockingStrategy : IPieceBlockingStrategy
{
    public bool PieceIsNotBlocking(Piece piece, string move, List<Piece> board)
    {
        if (piece.PiecePosition[0] != move[0] && piece.PiecePosition[1] != move[1])
        {
            return new BishopIsNotBlockingStrategy().PieceIsNotBlocking(piece, move, board);
        }
        else 
        {
            return new RookIsNotBlockingStrategy().PieceIsNotBlocking(piece, move, board);
        }
    }
}
public class KingIsNotBlockingStrategy : IPieceBlockingStrategy
{
    public bool PieceIsNotBlocking(Piece piece, string move, List<Piece> board)
    {
        return board.Find(p => p.PieceColor == piece.PieceColor && p.PiecePosition == move) is null;
    }
}