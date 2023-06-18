public abstract class MoveHandler : ISubject
{
    public MoveHandler(List<Piece> board)
    {
        _board = board;
    }
    protected MoveHandler _nextHandler;
    protected List<Piece> _board;

    public void SetNextHandler(MoveHandler handler)
    {
        _nextHandler = handler;
    }

    public virtual bool HandleMove(Piece piece, string move)
    {
        if (_nextHandler != null)
        {
            return _nextHandler.HandleMove(piece, move);
        }
        else
        {
            return true;
        }
    }

    public virtual bool MoveAllowedRequest(Piece piece, string move)
    {
        return HandleMove(piece, move);
    }
}
public class BlockingPieceHandler : MoveHandler
{
    private BlockingStrategyFactory _factory;
    public BlockingPieceHandler(List<Piece> board, BlockingStrategyFactory factory) : base(board)
    {
        _factory = factory;
    }
    public override bool HandleMove(Piece piece, string move)
    {
        if (_factory[piece.PieceName].PieceIsNotBlocking(piece, move, _board))
        {
            if (_nextHandler != null)
            {
                return _nextHandler.HandleMove(piece, move);
            }
            else
            {
                System.Console.WriteLine("Piece is not blocking.");
                return true;
            }
        }
        else
        {
            System.Console.WriteLine("Piece is blocking.");
            return false;
        }
    }

    public override bool MoveAllowedRequest(Piece piece, string move)
    {
        return base.MoveAllowedRequest(piece, move);
    }
}
public class KingSafetyHandler : MoveHandler
{
    private BlockingStrategyFactory _factory;
    private ChecksCollection _collection;
    public KingSafetyHandler(List<Piece> board, BlockingStrategyFactory factory) : base(board)
    {
        _factory = factory;
    }
    public override bool HandleMove(Piece piece, string move)
    {
        List<Piece> tempBoard = _board;

        tempBoard.Remove(piece);
        piece.PiecePosition = move;
        tempBoard.Add(piece);

        _collection = new ChecksCollection();

        using (var a = _collection.GetEnumerator())
        {
            while (a.MoveNext())
            {
                var checker = a.Current.Invoke(tempBoard, piece.PieceColor, _factory);
                if (checker is not null)
                {
                    System.Console.WriteLine($"King is under a check. {checker.PieceName} {checker.PiecePosition}");
                    return false;
                }
            }
        }
        if (_nextHandler is not null)
        {
            return _nextHandler.HandleMove(piece, move);
        }
        else
        {
            System.Console.WriteLine("King is safe.");
            return true;
        }
    }

    public override bool MoveAllowedRequest(Piece piece, string move)
    {
        return base.MoveAllowedRequest(piece, move);
    }
}