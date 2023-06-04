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
            return _nextHandler.HandleMove(piece, move);
        }
        else
        {
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
    private ChecksCollection _collection;
    public KingSafetyHandler(List<Piece> board, ChecksCollection collection) : base(board)
    {
        _collection = collection;
    }
    public override bool HandleMove(Piece piece, string move)
    {
        using (var a = _collection.GetEnumerator())
        {
            while (a.MoveNext())
            {
                if (a.Current.Invoke(_board, piece.PieceColor) is not null)
                {
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
            return false;
        }
    }

    public override bool MoveAllowedRequest(Piece piece, string move)
    {
        return base.MoveAllowedRequest(piece, move);
    }
}