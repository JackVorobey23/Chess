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
            if (piece.PieceColor == PieceColor.White)
            {
                for (int i = piece.PiecePosition[1] + 1; i <= move[1]; i++)
                {
                    if (board.Find(p => p.PiecePosition[1] == i && p.PiecePosition[0] == piece.PiecePosition[0]) is not null)
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int i = piece.PiecePosition[1] - 1; i >= move[1]; i--)
                {
                    if (board.Find(p => p.PiecePosition[1] == i && p.PiecePosition[0] == piece.PiecePosition[0]) is not null)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
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
        int startPoint = piece.PiecePosition[0];
        var sycleCond = (int i) => (i < piece.PiecePosition[0], i);

        if (move[0] == piece.PiecePosition[0])
        {
            if (move[1] > piece.PiecePosition[1])
            {
                sycleCond = (i) => (i <= move[1], 1);
                startPoint = piece.PiecePosition[1] + 1;
            }
            else
            {
                sycleCond = (i) => (i >= move[1], -1);
                startPoint = piece.PiecePosition[1] - 1;
            }
        }
        else
        {
            if (move[0] > piece.PiecePosition[0])
            {
                sycleCond = (i) => (i <= move[0], 1);
                startPoint = piece.PiecePosition[0] + 1;
            }
            else
            {
                sycleCond = (i) => (i >= move[0], -1);
                startPoint = piece.PiecePosition[0] - 1;
            }
        }
        for (int i = startPoint; sycleCond.Invoke(i).Item1; i += sycleCond(i).Item2)
        {
            Piece blockingPiece = new Piece(PieceName.Pawn, "", PieceColor.White);
            if (move[0] == piece.PiecePosition[0])
            {
                blockingPiece = board.Find(p => p.PiecePosition == $"{(char)piece.PiecePosition[0]}{(char)(i)}");
            }
            else
            {
                blockingPiece = board.Find(p => p.PiecePosition == $"{(char)(i)}{(char)piece.PiecePosition[1]}");
            }
            if (blockingPiece is not null)
            {
                if (blockingPiece.PiecePosition == move && piece.PieceColor != blockingPiece.PieceColor)
                {
                    continue;
                }
                return false;
            }
        }
        return true;

    }
}
public class BishopIsNotBlockingStrategy : IPieceBlockingStrategy
{
    public bool PieceIsNotBlocking(Piece piece, string move, List<Piece> board)
    {
        for (int i = 1; i <= Math.Abs(move[0] - piece.PiecePosition[0]); i++)
        {
            int k = move[0] < piece.PiecePosition[0] ? -1 : 1;
            int n = move[1] < piece.PiecePosition[1] ? -1 : 1;

            int hc = piece.PiecePosition[0] + k * i;
            int vc = piece.PiecePosition[1] + n * i;
            Piece blockingPiece = board.Find(p => p.PiecePosition[0] == (char)hc && p.PiecePosition[1] == (char)vc);

            if (blockingPiece is not null)
            {
                if (blockingPiece.PiecePosition == move && piece.PieceColor != blockingPiece.PieceColor)
                {
                    continue;
                }
                return false;
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